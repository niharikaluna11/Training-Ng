package main

import (
	"fmt"
	"mygoapp/model"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
)

func AddCustomer(ctx *gin.Context) {
	if logger == nil {
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Internal server error: Logger not initialized"})
		return
	}

	var customer model.Customer
	if err := ctx.ShouldBindJSON(&customer); err != nil {
		logger.Error("Invalid request payload", zap.Error(err))
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Invalid request payload", "error": err.Error()})
		return
	}
	logger.Info("Received Customer Request", zap.String("FirstName", customer.FirstName))

	if customer.AccountNumber == "" {
		ctx.JSON(http.StatusBadRequest, gin.H{"message": "Account Number is required"})
		return
	}

	

	newCustomer := &model.Customer{
		FirstName:     customer.FirstName,
		LastName:      customer.LastName,
		Address:       customer.Address,
		AccountNumber: customer.AccountNumber,
		Email:         customer.Email,
		PhoneNumber:   customer.PhoneNumber,
		AccountType:   customer.AccountType,
		CStatus:       model.Activated,
	}

	if err := bankAppDbConnector.Create(newCustomer).Error; err != nil {
		logger.Error("Failed to Add Customer", zap.Error(err))
		ctx.JSON(http.StatusInternalServerError, gin.H{"message": "Failed to create customer", "error": err.Error()})
		return
	}

	logger.Info(fmt.Sprintf("Customer %s created successfully", customer.FirstName))
	ctx.JSON(http.StatusCreated, gin.H{"message": "Customer Created Successfully!"})

}
