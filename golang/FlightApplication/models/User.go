package models

type User struct {
	ID   int
	Name string
	Role UserRole
}

type UserRole int

const (
	Admin      UserRole = iota //0
	NormalUser                 //1
)
