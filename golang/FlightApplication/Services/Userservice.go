package service

import (
	"fmt"
	"os"
)

func UserService() {
	fmt.Println("Hello There!")
	for {
		fmt.Println("1. Register")
		fmt.Println("2. Login")
		fmt.Println("Press 0 to Exit")
		fmt.Println("--------------------------------------")
		var choice int
		fmt.Print("Enter your choice: ")
		_, err := fmt.Scan(&choice)
		if err != nil {
			fmt.Println("Invalid input, please enter a number.")
			continue
		}

		switch choice {
		case 1:
			UserRegister()
		case 2:
			UserLogin()
		case 0:
			fmt.Println("Exiting...")
			os.Exit(0)
		default:
			fmt.Println("Invalid choice. Please try again.")
		}
	}
}
