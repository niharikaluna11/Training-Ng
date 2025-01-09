package main

import (
	"myModule/model"
	"net/http"
	"strings"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"golang.org/x/crypto/bcrypt"
)

func Login(ctx *gin.Context) {
	var user model.User
	ctx.ShouldBindJSON(&user)

	userEmail := user.Email
	userPassword := user.Password

	// * 1. validation logic
	if userEmail == "" || userPassword == "" || !strings.Contains(userEmail, "@") || !strings.Contains(userEmail, ".") || len(userPassword) < 6 {
		logger.Warn("Invalid Request", zap.String("user email", userEmail))
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "The request contains missing or invalid fields"})
		return
	}
	// * 2. Check if the user exists
	var existingUser model.User
	userNotFoundError := flightDbConnector.Where("email = ?", userEmail).First(&existingUser).Error

	if userNotFoundError != nil {
		logger.Error("User not found", zap.String("user email", userEmail))
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "Invalid credentials"})
		return
	}

	err := bcrypt.CompareHashAndPassword([]byte(existingUser.Password), []byte(userPassword))
	if err != nil {
		logger.Warn("Authentication failed due to wrong password")
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Authentication failed due to wrong password"})
		return
	}

	logger.Info("User Authenticated Successfully", zap.String("username", existingUser.Name), zap.String("useremail", userEmail))
	token, err := jwtManager.GeneratingToken(&existingUser)
	if err != nil {
		logger.Error("Failed to generate token", zap.String("user email", userEmail))
		ctx.JSON(http.StatusInternalServerError, gin.H{"token failure": "Couldn't generate the token"})
		return
	}
	ctx.JSON(http.StatusOK, gin.H{"message": "user login successfully", "token": token})
}
