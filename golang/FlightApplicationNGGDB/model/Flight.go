package model

import (
	"time"

	"gorm.io/gorm"
)

type Flights struct {
	gorm.Model
	FlightNumber     string       `gorm:"type:varchar(255);uniqueIndex"` // Unique constraint for FlightNumber
	DepartureAirport string       `gorm:"not null"`                      // Ensure not null
	ArrivalAirport   string       `gorm:"not null"`                      // Ensure not null
	DepartureTime    time.Time    `gorm:"not null"`                      // Ensure not null
	ArrivalTime      time.Time    `gorm:"not null"`                      // Ensure not null
	Airline          string       `gorm:"not null"`                      // Ensure not null
	FlightType       string       `gorm:"not null"`                      // Ensure not null
	Seats            int          `gorm:"not null"`                      // Ensure not null
	FlightStatus     FlightStatus `gorm:"not null"`                      // Ensure not null
	Fare             float64      `gorm:"not null"`                      // Ensure not null (corrected spelling to Fare)
	FlightDate       time.Time    `gorm:"not null"`                      // Ensure not null
	UserID           string       `gorm:"type:varchar(255)"`             // Removed unique constraint; user can create multiple flights
}

type FlightStatus int

const (
	Scheduled FlightStatus = iota // Flight is scheduled
	Completed                     // Flight has been completed
	Cancelled                     // Flight has been cancelled
)
