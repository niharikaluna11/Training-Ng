package arrayandslices

import (
	"fmt"
)

func ArrayDemo() {
	//fixed length ds

	for {

		fmt.Println("Select an option:")

		fmt.Println("1. Array ")

		fmt.Println("0. Exit")

		var choice int
		fmt.Print("Enter your choice: ")
		fmt.Scan(&choice)

		switch choice {
		case 1:
			fmt.Println("Array Example")

		case 0:
			fmt.Println("Exiting...")
			return

		default:

			fmt.Println("Invalid choice. Please try again.")
			fmt.Println("Exiting...")
			return
		}
	}

}
