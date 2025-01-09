package main

import (
	"auth-micro/config"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

var logger *zap.Logger
var userDbConnector *gorm.DB

func init() {
	var err error
	logger, err = zap.NewDevelopment()

	if err != nil {
		panic(err)
	}
	defer logger.Sync()
}

func main() {
	userDbConnector = config.ConnectDB()

	// configuration of the http server.
	httpServer := gin.Default()
	//? Method : @POST
	// ? Endpoint Route : /save-user
	httpServer.POST("/save-user", AddUser)
	httpServer.GET("/hi", func(ctx *gin.Context) {
		ctx.JSON(http.StatusCreated, gin.H{"message": "User created successfully"})
	})

	// running the server
	httpServer.Run(":8080")
}
