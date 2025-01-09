package model

import (
	"time"

	"gorm.io/gorm"
)

type Flights struct {
	gorm.Model
	FlightNumber     string    `gorm:"type:varchar(255);uniqueIndex:user_flight_unique"`
	DepartureAirport string    `gorm:"not null"`
	ArrivalAirport   string    `gorm:"not null"`
	DepartureTime    time.Time `gorm:"not null"`
	ArrivalTime      time.Time `gorm:"not null"`
	Airline          string    `gorm:"not null"`
	AircraftType     string    `gorm:"not null"`
	FlightStatus     string    `gorm:"not null"`
	Fair             float64
	FlightDate       time.Time
	UserId           string `gorm:"type:varchar(255);uniqueIndex:user_flight_unique"`
}
