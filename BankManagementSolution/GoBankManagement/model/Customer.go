package model

import "gorm.io/gorm"

type AccountType int

const (
	Savings      AccountType = iota //0
	Checking                        //1
	FixedDeposit                    //2
)

type CustomerStatus int

const (
	Activated   CustomerStatus = iota //0
	Deactivated                       //1
)

type Customer struct {
	gorm.Model
	CustomerID    int            `gorm:"primaryKey"`
	AccountNumber string         `gorm:"not null;unique"`
	FirstName     string         `gorm:"not null"`
	LastName      string         `gorm:"not null"`
	Email         string         `gorm:"not null;unique"`
	PhoneNumber   string         `gorm:"not null"`
	Address       string         `gorm:"not null"`
	AccountType   AccountType    `gorm:"not null"`
	CStatus       CustomerStatus `gorm:"default:0"`
	
}
