package models

type UserFlight struct {
	ID       int    // Unique identifier for the booking
	UserID   int    // Foreign key linking to the User table
	FlightID string // Foreign key linking to the Flight table
}
