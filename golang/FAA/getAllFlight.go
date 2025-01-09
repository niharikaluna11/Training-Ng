package main

import (
	"myModule/model"
	"net/http"

	"github.com/gin-gonic/gin"
)

func GetAllFlight(ctx *gin.Context) {
	var flights []model.Flights

	if err := flightDbConnector.Find(&flights).Error; err != nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"error": "Unable to retrieve flights"})
		return
	}
	totalFlights := len(flights)
	if totalFlights == 0 {
		ctx.JSON(http.StatusOK, gin.H{
			"message": "No Flights",
		})
		return
	}
	ctx.JSON(http.StatusOK, gin.H{"totalFlight": totalFlights, "flights": flights})
}
