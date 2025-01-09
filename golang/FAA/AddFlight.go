package main

import (
	"fmt"
	"myModule/model"
	"net/http"
	"strconv"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"gorm.io/gorm"
)

func AddFlight(ctx *gin.Context) {
	var flight model.Flights
	ctx.ShouldBindJSON(&flight)

	// * 1 Get User Email from Claims
	userEmail := ctx.GetString("userEmail")
	if userEmail == "" {
		logger.Error("failed to get the token from the header")
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "failed to get the token from the header"})
		return
	}

	// * 2. Check user role
	userRole := ctx.GetString("userRole")
	if userRole != "admin" {
		logger.Error("User is not authorized to add a flight")
		ctx.JSON(http.StatusUnauthorized, gin.H{"message": "User is not authorized to add a flight"})
		return
	}

	// * 3. Check for Existing Flight.
	// var existingFlight flightstructs.FlightStruct
	var existingFlight model.Flights

	flightNotFoundError := flightDbConnector.Where("flight_number = ?", flight.FlightNumber).First(&existingFlight).Error

	if flightNotFoundError == gorm.ErrRecordNotFound {

		// * 4. Get the user from the database
		var user model.User
		flightDbConnector.Where("email = ?", userEmail).First(&user)

		// * 5. adding the user id inside the flight
		flight.UserId = strconv.FormatUint(uint64(user.ID), 10)

		primaryKey := flightDbConnector.Create(&flight)

		if primaryKey.Error != nil {
			logger.Error("Failed to Add Flight", zap.String("flight number ", flight.FlightNumber), zap.Error(primaryKey.Error))
			ctx.JSON(http.StatusConflict, gin.H{"message": "The Flight is already added"})
			return
		}
		// if err := flightDbConnector.Create(&flight).Error; err != nil {
		// 	logger.Error("Error creating flight", zap.Error(err))
		// 	ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Error creating flight"})
		// 	return
		// }
		logger.Info(fmt.Sprintf("flight %s created successfully", flight.FlightNumber))
		ctx.JSON(http.StatusCreated, gin.H{"message": "Flight added successfully"})

	} else {
		logger.Warn("User Flight Already Exist", zap.String("flight number", flight.FlightNumber))
		ctx.JSON(http.StatusConflict, gin.H{"message": "Flight Already Exist"})
	}

}
