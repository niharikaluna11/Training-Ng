package model

import "gorm.io/gorm"

type Booking struct {
	gorm.Model
	BookingID string  `gorm:"type:varchar(255);uniqueIndex"`
	UserID    string  `gorm:"not null"`
	User      User    `gorm:"foreignKey:UserID"` // Define relationship
	FlightID  string  `gorm:"not null"`
	Flight    Flights `gorm:"foreignKey:FlightID"` // Define relationship
}
