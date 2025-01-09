package main

import (
	"myModule/config"
	"myModule/jwt"
	"net/http"
	"time"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)


var logger *zap.Logger
var flightDbConnector *gorm.DB
var jwtManager *jwt.JWTManager

func init() {
	var err error
	logger, err = zap.NewDevelopment()

	if err != nil {
		panic(err)
	}
	defer logger.Sync()
}

func main() {
	flightDbConnector = config.ConnectDB()

	// * Create a new jwt manager
	jwtManager = jwt.NewJWTManager("SECRET_KEY", 5*time.Hour)

	// configuration of the http server.
	httpServer := gin.Default()

	// ? Unprotected Routes
	httpServer.POST("/save-user", AddUser)
	httpServer.POST("/login-user", Login)
	httpServer.GET("/hi", func(ctx *gin.Context) {
		ctx.JSON(http.StatusCreated, gin.H{"message": "User created successfully"})
	})

	// ? Protected Routes
	httpServer.Use(jwt.AuthorizeJwtToken())
	httpServer.POST("/add-flight", AddFlight)
	httpServer.GET("/getall-flight", GetAllFlight)

	// running the server
	httpServer.Run(":8081")
}
