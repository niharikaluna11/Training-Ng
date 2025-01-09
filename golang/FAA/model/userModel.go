package model

import "gorm.io/gorm"

type User struct {
	gorm.Model
	Name     string
	Password string
	Email    string `gorm:"unique"`
	Phone    string `gorm:"unique"`
	Address  string
	City     string
	Role     string
}
