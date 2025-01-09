package flightstructs

import (
	"fmt"
	"sort"
)

type FlightStruct struct {
	FlightNumber  string
	DepartureFrom string
	ArrivalTo     string
	FlightDate    string
	Fair          float64
}

type Flights struct {
	flights []FlightStruct
}

func (flight *Flights) getAllFlight() {
	fmt.Println("All Flights:")
	fmt.Println("Flight Number\t", "Departure\t", "Arrival\t", "Fair\t", "Date")
	for _, txn := range flight.flights {
		fmt.Println(txn.FlightNumber, "\t\t", txn.DepartureFrom, " \t", txn.ArrivalTo, "         ", txn.Fair, "  ", txn.FlightDate)
	}
}

func searchFlight(flights *Flights, DepartureFrom string, ArrivalTo string) Flights {
	var sortFlight Flights
	for _, txn := range flights.flights {
		if txn.DepartureFrom == DepartureFrom && txn.ArrivalTo == ArrivalTo {
			sortFlight.flights = append(sortFlight.flights, txn)
		}
	}
	return sortFlight
}

func createFlightStruct(flights *Flights) {
	flights.flights = append(flights.flights,
		FlightStruct{
			FlightNumber:  "AE123",
			DepartureFrom: "Noida",
			ArrivalTo:     "Goa",
			FlightDate:    "15-12-2024/10:30",
			Fair:          2799,
		},
		FlightStruct{
			FlightNumber:  "AE124",
			DepartureFrom: "Delhi",
			ArrivalTo:     "Goa",
			FlightDate:    "16-12-2024/09:00",
			Fair:          2999,
		},
		FlightStruct{
			FlightNumber:  "AE125",
			DepartureFrom: "Goa",
			ArrivalTo:     "Mumbai",
			FlightDate:    "17-12-2024/10:00",
			Fair:          3590,
		},
		FlightStruct{
			FlightNumber:  "AE126",
			DepartureFrom: "Mumbai",
			ArrivalTo:     "Delhi",
			FlightDate:    "15-12-2024/20:30",
			Fair:          2499,
		},
	)
}

func SortByFair(flights *Flights) {
	sort.Slice(flights.flights, func(i, j int) bool {
		return flights.flights[i].Fair > flights.flights[j].Fair
	})
	flights.getAllFlight()
}

func DeleteFlight(flights *Flights, flightNumber string, flightDate string) {
	fmt.Println(flightDate, flightNumber)
	for index, txn := range flights.flights {
		if txn.FlightNumber == flightNumber && txn.FlightDate == flightDate {
		
			flights.flights = append(flights.flights[:index], flights.flights[index+1:]...)
		}
	}
}

func adminMenu(flights *Flights) {
	var choice int
	for {
		fmt.Println("\nAdmin Menu:")
		fmt.Println("1. See All Flights")
		fmt.Println("2. Add Flight")
		fmt.Println("3. Delete Flight")
		fmt.Println("0. Exit")
		fmt.Print("Choose an option: ")
		fmt.Scanln(&choice)

		switch choice {
		case 1:
			flights.getAllFlight()

		case 2:
			var flightNumber, DepartureFrom, ArrivalTo, flightDate string
			var Fair float64
			fmt.Print("Enter Flight Number: ")
			fmt.Scanln(&flightNumber)
			fmt.Print("Enter Departure City: ")
			fmt.Scanln(&DepartureFrom)
			fmt.Print("Enter Arrival City: ")
			fmt.Scanln(&ArrivalTo)
			fmt.Print("Enter Flight Date: ")
			fmt.Scanln(&flightDate)
			fmt.Print("Enter Flight Fare: ")
			fmt.Scanln(&Fair)

			newFlight := FlightStruct{
				FlightNumber:  flightNumber,
				DepartureFrom: DepartureFrom,
				ArrivalTo:     ArrivalTo,
				FlightDate:    flightDate,
				Fair:          Fair,
			}
			flights.flights = append(flights.flights, newFlight)
			fmt.Println("Flight added successfully.")

		case 3:
			var fligntNumber, flightDate string
			fmt.Print("Enter the Flight Number to be delete: ")
			fmt.Scanln(&fligntNumber)
			fmt.Print("Enter the Flight Date to be delete: ")
			fmt.Scanln(&flightDate)
			DeleteFlight(flights, fligntNumber, flightDate)
		case 0:
			fmt.Println("Admin Logged Out.")
			return

		default:
			fmt.Println("Invalid option. Please choose a valid option.")
		}
	}
}

func customerMenu(flights *Flights) {
	var choice int
	for {
		fmt.Println("\nCustomer Menu:")
		fmt.Println("1. See All Flights")
		fmt.Println("2. Search Flight")
		fmt.Println("3. Sort By Flight Fair")

		fmt.Println("0. Exit")
		fmt.Print("Choose an option: ")
		fmt.Scanln(&choice)

		switch choice {
		case 1:
			flights.getAllFlight()

		case 2:
			var DepartureFrom, ArrivalTo string
			fmt.Print("From: ")
			fmt.Scanln(&DepartureFrom)
			fmt.Print("To: ")
			fmt.Scanln(&ArrivalTo)
			if DepartureFrom != "" && ArrivalTo != "" {
				result := searchFlight(flights, DepartureFrom, ArrivalTo)
				if len(result.flights) > 0 {
					fmt.Println("\nMatching Flights:")
					for _, txn := range result.flights {
						fmt.Println(txn.FlightNumber, txn.DepartureFrom, txn.ArrivalTo, txn.Fair, txn.FlightDate)
					}

					var sortChoice int
					fmt.Println("\n1. Sort by Price")
					fmt.Println("2. Sort by Flight Date")
					fmt.Println("0. Exit")
					fmt.Print("Choose an option: ")
					fmt.Scanln(&sortChoice)

					switch sortChoice {
					case 1:
						sort.Slice(result.flights, func(i, j int) bool {
							return result.flights[i].Fair > result.flights[j].Fair
						})
						fmt.Println("\nSorted by Price:")
						for _, txn := range result.flights {
							fmt.Println(txn.FlightNumber, txn.DepartureFrom, txn.ArrivalTo, txn.Fair, txn.FlightDate)
						}
					case 2:
						sort.Slice(result.flights, func(i, j int) bool {
							return result.flights[i].FlightDate > result.flights[j].FlightDate
						})
						fmt.Println("\nSorted by Flight Date:")
						for _, txn := range result.flights {
							fmt.Println(txn.FlightNumber, txn.DepartureFrom, txn.ArrivalTo, txn.Fair, txn.FlightDate)
						}
					}
				} else {
					fmt.Println("\nNo matching flights found.")
				}
			}

		case 3:
			SortByFair(flights)

		case 0:
			fmt.Println("Goodbye!")
			return

		default:
			fmt.Println("Invalid option. Please choose a valid option.")
		}
	}
}

func FlightMainFunction() {
	flights := Flights{}
	fmt.Println("\nFlight Booking App")
	createFlightStruct(&flights)

	var userType int
	fmt.Println("\n1. Admin")
	fmt.Println("2. Customer")
	fmt.Print("Choose user type: ")
	fmt.Scanln(&userType)

	switch userType {
	case 1:
		adminMenu(&flights)
	case 2:
		customerMenu(&flights)
	default:
		fmt.Println("Invalid option. Please choose a valid user type.")
	}
}
