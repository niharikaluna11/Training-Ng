package main

import (
	"fmt"
	service "mymod/Services"
	"os"
)

func main() {
	fmt.Println("--------------------------------------")
	fmt.Println("Hello ! Welcome To Flight Service")
	for {
		fmt.Println("Are you ?")
		fmt.Println("1. User")
		fmt.Println("2. Admin")
		fmt.Println("Press 0 to Exit")
		fmt.Println("--------------------------------------")
		var choice int
		fmt.Print("Enter your choice: ")
		fmt.Scan(&choice)

		switch choice {
		case 1:
			service.UserService()
		case 2:
			service.UserLogin()
		case 0:
			fmt.Println("Exiting...")
			os.Exit(0)
		default:
			fmt.Println("Invalid choice. Please try again.")
			main()
		}
	}
}
