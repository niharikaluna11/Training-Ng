package model

import "gorm.io/gorm"

type User struct {
	gorm.Model
	UserID   string   `gorm:"type:varchar(255);uniqueIndex"` // Changed to consistent "UserID" naming
	Name     string   `gorm:"not null"`                      // Name should not be null
	Password string   `gorm:"not null"`                      // Password should not be null
	Email    string   `gorm:"unique;not null"`               // Email should be unique and not null
	Phone    string   `gorm:"unique;not null"`               // Phone should be unique and not null
	Address  string   `gorm:"not null"`                      // Address should not be null
	City     string   `gorm:"not null"`                      // City should not be null
	Role     UserRole `gorm:"not null"`                      // Role should not be null
}

type UserRole int

const (
	Admin      UserRole = iota // User role for admin
	NormalUser                 // User role for a normal user
)
