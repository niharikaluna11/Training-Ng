package service

import (
	"fmt"
	"mymod/models"
	// "time"
)

var Flights = []models.Flight{} // List to store added flights

func init() {

	Flights = append(Flights, models.Flight{
		ID:          1,
		FlightId:    "AE101",
		Source:      "delhi",
		Destination: "goa",
		// Departure:   departure,
		// Arrival:     arrival,
		Seats:   10,
		Price:   8900.76,
		Airline: "Indigo",
		IsGoing: models.Scheduled,
	})

	Flights = append(Flights, models.Flight{
		ID:          2,
		FlightId:    "AE102",
		Source:      "delhi",
		Destination: "jaipur",
		// Departure:   departure,
		// Arrival:     arrival,
		Seats:   3,
		Price:   5678.98,
		Airline: "Indigo",
		IsGoing: models.Scheduled,
	})
}

var currentFlightID = 3

func AdminFunc() {
	for {

		fmt.Println("\nAdmin Functionality:")
		fmt.Println("1. Add Flight")
		fmt.Println("2. View Flights")
		fmt.Println("3. Cancel Flight")
		fmt.Println("Press 0 to Exit")
		fmt.Println("--------------------------------------")
		var choice int
		fmt.Print("Enter your choice: ")
		fmt.Scan(&choice)

		switch choice {
		case 1:
			AddFlight()
		case 2:
			ViewFlightOptions()
		case 3:
			DeleteFlight()
		case 0:
			fmt.Println("Exiting Admin functionality.")
			return
		default:
			fmt.Println("Invalid choice. Please try again.")
		}
	}
}

func ViewFlightOptions() {
	fmt.Println("1. View All Flights")
	fmt.Println("2. View Available Flights")
	var choice int
	fmt.Print("Enter your choice: ")
	fmt.Scan(&choice)
	switch choice {
	case 1:
		ViewFlights()
	case 2:
		ViewFlightsAv()
	default:
		fmt.Println("Invalid choice.")
	}
}

func AddFlight() {
	var source, destination, flightid, airline string
	var seats int
	var price float64
	// var departureStr, arrivalStr string

	fmt.Println("\nEnter flight details:")
	fmt.Println("--------------------------------------")
	// Take input from admin
	fmt.Print("Enter Flight Id (e.g., AE101): ")
	fmt.Scan(&flightid)

	fmt.Print("Enter Source (e.g., Delhi): ")
	fmt.Scan(&source)

	fmt.Print("Enter Destination (e.g., Jaipur): ")
	fmt.Scan(&destination)

	fmt.Print("Enter Seats (e.g., 300): ")
	fmt.Scan(&seats)

	// // Taking time input in specific format
	// fmt.Print("Enter Departure time (yyyy-mm-dd hh:mm): ")
	// fmt.Scan(&departureStr)

	// fmt.Print("Enter Arrival time (yyyy-mm-dd hh:mm): ")
	// fmt.Scan(&arrivalStr)

	// // Parse the time strings into time.Time objects
	// departure, err := time.Parse("2006-01-02 15:04", departureStr)
	// if err != nil {
	// 	fmt.Println("Error parsing departure time:", err)
	// 	return
	// }

	// arrival, err := time.Parse("2006-01-02 15:04", arrivalStr)
	// if err != nil {
	// 	fmt.Println("Error parsing arrival time:", err)
	// 	return
	// }

	fmt.Print("Enter Price (e.g., 4000.00): ")
	fmt.Scan(&price)

	fmt.Print("Enter Airline (e.g., Indigo): ")
	fmt.Scan(&airline)

	// Create a new flight with details
	flight := models.Flight{
		ID:          currentFlightID,
		FlightId:    flightid,
		Source:      source,
		Destination: destination,
		// Departure:   departure,
		// Arrival:     arrival,
		Seats:   seats,
		Price:   price,
		Airline: airline,
		IsGoing: models.Scheduled,
	}

	currentFlightID++

	// Append the new flight to the flights slice
	Flights = append(Flights, flight)
	fmt.Println("--------------------------------------")
	// Display the flight details properly formatted
	fmt.Printf("\nFlight added successfully:\n")
	fmt.Printf("Flight Id: %s", flight.FlightId)
	fmt.Printf(" OF Airline: %s\n", flight.Airline)
	fmt.Printf("From: %s ", flight.Source)
	fmt.Printf(" To: %s ", flight.Destination)
	fmt.Printf(" Price: %.2f", flight.Price)
	fmt.Printf(" Status: %s\n", fs(flight.IsGoing))
	fmt.Println("--------------------------------------")
}

func fs(status models.FlightStatus) string {
	switch status {
	case models.Scheduled:
		return "Scheduled"
	case models.Completed:
		return "Completed"
	case models.Cancelled:
		return "Cancelled"
	default:
		return "Unknown"
	}
}
func ViewFlights() {
	fmt.Println("--------------------------------------")
	fmt.Println("\nList of All Flights:")
	// Loop through all flights and print their details
	for _, flight := range Flights {
		fmt.Printf("Flight ID: %s\n", flight.FlightId)
		fmt.Printf("Airline: %s\n", flight.Airline)
		fmt.Printf("From: %s\n", flight.Source)
		fmt.Printf("To: %s\n", flight.Destination)
		fmt.Printf("Seats Available: %d\n", flight.Seats)
		fmt.Printf("Price: %.2f\n", flight.Price)
		fmt.Printf("Status: %s\n", fs(flight.IsGoing)) // Use String method for status
		fmt.Println("--------------------------------------")
	}
}

func ViewFlightsAv() {
	availableFound := false // Flag to track if any available flights are found
	fmt.Println("\nList of Available Flights:")
	// Loop through flights and check for available ones (status Scheduled)
	for _, flight := range Flights {
		if flight.IsGoing == models.Scheduled { // Only show scheduled flights
			availableFound = true
			fmt.Printf("Flight ID: %s\n", flight.FlightId)
			fmt.Printf("Airline: %s\n", flight.Airline)
			fmt.Printf("From: %s\n", flight.Source)
			fmt.Printf("To: %s\n", flight.Destination)
			fmt.Printf("Seats Available: %d\n", flight.Seats)
			fmt.Printf("Price: %.2f\n", flight.Price)
			fmt.Printf("Status: %s\n", fs(flight.IsGoing)) // Use String method for status
			fmt.Println("--------------------------------------")
		}
	}
	// If no available flights, print a message
	if !availableFound {
		fmt.Println("No available flights found.")
	}
}

// func ifFlightExist(flightidn string) {
// 	availableFound := false // Flag to track if any available flights are found

// }

func DeleteFlight() {
	var flightID string
	fmt.Print("Enter the flight ID to cancel: ")
	fmt.Scan(&flightID)

	var found bool
	for i, flight := range Flights {
		if flight.FlightId == flightID {
			if Flights[i].IsGoing == models.Cancelled {
				fmt.Printf("Flight ID %s is already cancelled.\n", flightID)
				return
			}
			// Soft delete by marking the flight status as Cancelled
			Flights[i].IsGoing = models.Cancelled
			fmt.Printf("Flight ID %s has been cancelled.\n", flightID)
			found = true
			break
		}
	}

	if !found {
		fmt.Println("Flight ID not found.\n")
	}
}
