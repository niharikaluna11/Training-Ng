package service

import (
	"fmt"
	"mymod/models"
	// "time"
)

var UserFlight = []models.UserFlight{}
var currentBookingID = 101

func UserFunc(user models.User) {
	for {

		fmt.Println("\n User Functionality:")
		fmt.Println("1. See All Available Flight")
		fmt.Println("2. Search Available Flight Basis on S&D")
		fmt.Println("2. Book Available Flight by its id")

		fmt.Println("Press 0 to Exit")
		fmt.Println("--------------------------------------")
		var choice int
		fmt.Print("Enter your choice: ")
		fmt.Scan(&choice)

		switch choice {
		case 1:
			ViewFlightsAv()
		case 2:
			SearchFlightsAv()
		case 3:
			BookFlightById(user.ID)
		case 0:
			fmt.Println("Exiting Admin functionality.")
			return
		default:
			fmt.Println("Invalid choice. Please try again.")
		}
	}
}

func BookFlightById(userId int) {
	var flightID string
	fmt.Print("Enter the flight ID to book: ")
	fmt.Scan(&flightID)

	var seatsno int
	fmt.Print("Enter the number of seats: ")
	fmt.Scan(&seatsno)

	var found bool
	for i, flight := range Flights {
		if flight.FlightId == flightID {
			found = true
			if flight.IsGoing == models.Cancelled {
				fmt.Println("Flight is cancelled.")
				break
			}
			if flight.Seats > 0 && flight.Seats > seatsno {

				Flights[i].Seats = Flights[i].Seats - seatsno

				// Create a new UserFlight record
				bookingID := currentBookingID
				currentBookingID++

				newBooking := models.UserFlight{
					ID:       bookingID,
					UserID:   userId,
					FlightID: flightID,
				}
				UserFlight = append(UserFlight, newBooking)

				fmt.Printf("Booking successful! Booking ID: %d\n", bookingID)
			} else {
				fmt.Println("Insufficient Seats.")
			}
			break
		}
	}

	if !found {
		fmt.Println("Flight ID not found.")
	}
}

func SearchFlightsAv() {
	var source, destination string
	fmt.Print("Enter Source (e.g., Delhi): ")
	fmt.Scan(&source)

	fmt.Print("Enter Destination (e.g., Jaipur): ")
	fmt.Scan(&destination)

	// Search for flights between the source and destination
	searchFlights(source, destination)
}

func searchFlights(source, destination string) {
	availableFound := false
	fmt.Println("\nList of Available Flights:")

	for _, flight := range Flights {
		if flight.Source == source && flight.Destination == destination && flight.IsGoing == models.Scheduled {
			availableFound = true
			fmt.Printf("Flight ID: %s\n", flight.FlightId)
			fmt.Printf("Airline: %s\n", flight.Airline)
			fmt.Printf("From: %s\n", flight.Source)
			fmt.Printf("To: %s\n", flight.Destination)
			fmt.Printf("Seats Available: %d\n", flight.Seats)
			fmt.Printf("Price: %.2f\n", flight.Price)
			fmt.Printf("Status: %s\n", fs(flight.IsGoing))
			fmt.Println("--------------------------------------")
		}
	}

	if !availableFound {
		fmt.Println("No available flights found.")
	}
}
