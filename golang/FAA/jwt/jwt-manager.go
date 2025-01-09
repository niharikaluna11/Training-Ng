package jwt

import (
	"fmt"
	"myModule/model"
	"net/http"
	"strings"
	"time"

	"github.com/dgrijalva/jwt-go"
	"github.com/gin-gonic/gin"
)

type JWTManager struct {
	secretKey     string
	tokenDuration time.Duration
}

type UserClaims struct {
	jwt.StandardClaims
	UserEmail string
	UserRole  string
}

// ? Create a new JWT Manager
func NewJWTManager(secretKey string, tokenDuration time.Duration) *JWTManager {
	return &JWTManager{
		secretKey:     secretKey,
		tokenDuration: tokenDuration,
	}
}

// ? Generate a token
func (jwtManager *JWTManager) GeneratingToken(user *model.User) (string, error) {
	// * 1. Prepare the UserClaim

	claims := UserClaims{
		StandardClaims: jwt.StandardClaims{
			ExpiresAt: time.Now().Add(jwtManager.tokenDuration).Unix(),
		},
		UserEmail: user.Email,
		UserRole:  user.Role,
	}
	// * create a token
	token := jwt.NewWithClaims(jwt.SigningMethodHS256, claims)
	return token.SignedString([]byte(jwtManager.secretKey))
}

// * Responsibility :- decode the token and get the userClaims and return the userEmail. (userClaims/userEmail);

func VerifyToken(accessToken string) (*UserClaims, error) {
	token, err := jwt.ParseWithClaims(
		accessToken,
		&UserClaims{},
		func(token *jwt.Token) (interface{}, error) {
			_, ok := token.Method.(*jwt.SigningMethodHMAC)

			if !ok {
				return nil, fmt.Errorf("unexpected token signing method")
			}
			return []byte("SECRET_KEY"), nil
		},
	)
	if err != nil {
		return nil, fmt.Errorf("invalid token %w", err)
	}
	claims, ok := token.Claims.(*UserClaims)
	if !ok {
		return nil, fmt.Errorf("invalid token %w", err)
	}
	return claims, nil
}

func AuthorizeJwtToken() gin.HandlerFunc {
	return func(ctx *gin.Context) {
		tokenString := ctx.GetHeader("Authorization")
		if len(tokenString) == 0 {
			ctx.JSON(http.StatusUnauthorized, gin.H{"jwt failure": "Authorization token is not provided."})
			ctx.Abort()
		}
		token := strings.Split(tokenString, " ")[1]
		claims, err := VerifyToken(token)

		if err != nil {
			ctx.JSON(http.StatusUnauthorized, gin.H{"jwt failure": "Authorization token is not valid."})
			ctx.Abort()
		}
		ctx.Set("userEmail", claims.UserEmail)
		ctx.Set("userRole", claims.UserRole)
		ctx.Next()
	}
}
