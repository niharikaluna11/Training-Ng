package main

import (
	"mygoapp/config" 
	"mygoapp/model"
	"net/http"

	"github.com/gin-contrib/cors"
	"github.com/gin-gonic/gin"
	"github.com/joho/godotenv"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

var logger *zap.Logger
var bankAppDbConnector *gorm.DB

func init() {
	var err error
	logger, err = zap.NewDevelopment()
	if err != nil {
		panic("Failed to initialize logger: " + err.Error())
	}
	defer logger.Sync()
}

func main() {

	if err := godotenv.Load(".env"); err != nil {
		panic("No .env file found")
	}

	bankAppDbConnector = config.ConnectDB()

	httpServer := gin.Default()

	httpServer.Use(cors.New(cors.Config{
		AllowOrigins:     []string{"*"},
		AllowMethods:     []string{"GET", "POST", "PUT", "DELETE"},
		AllowHeaders:     []string{"Origin", "Content-Type", "Authorization"},
		AllowCredentials: true,
	}))

	httpServer.POST("/add-customer", AddCustomer)

	httpServer.GET("/get-customers", func(ctx *gin.Context) {
		var customers []model.Customer
		if err := bankAppDbConnector.Find(&customers).Error; err != nil {
			ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve customers"})
			return
		}
		ctx.JSON(http.StatusOK, gin.H{"customer": customers})
	})

	httpServer.Run(":8090")
}
