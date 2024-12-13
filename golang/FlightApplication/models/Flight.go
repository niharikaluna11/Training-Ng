package models

type Flight struct {
	ID          int // Unique identifier for the flight
	FlightId    string
	Source      string // Starting point of the flight
	Destination string // Endpoint of the flight

	// Departure time.Time // Departure date and time
	// Arrival   time.Time // Arrival date and time

	Seats   int          // Total available seats
	Price   float64      // Ticket price
	Airline string       // Airline name
	IsGoing FlightStatus // Status of flight (e.g., active/in-progress)
}

//struct for flight

type FlightStatus int

const (
	Scheduled FlightStatus = iota // Flight is scheduled 0
	Completed                     // Flight has been completed 1
	Cancelled                     // Flight has been cancelled 2
)

//iota is a special keyword in Go used to generate a sequence of integer constants.
//iota increments the int FlightStatus
