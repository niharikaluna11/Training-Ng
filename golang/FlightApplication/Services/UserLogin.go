package service

import (
	"fmt"
	"mymod/models"
)

var Users = []models.User{}

func init() {
	Users = append(Users, models.User{ID: 1, Name: "niharika", Role: models.Admin})
	Users = append(Users, models.User{ID: 2, Name: "ng", Role: models.Admin})
}

var currentID = 3

func UserRegister() {
	fmt.Println("--------------------------------------")
	fmt.Println("User Registration")
	fmt.Print("Enter your name: ")
	var name string
	fmt.Scan(&name)

	// Check if the user already exists
	// if name == MainUser.Name {
	// 	fmt.Printf("User with name '%s' already exists as admin.\n", name)
	// 	return // No need to call UserService again, just return to the main menu
	// }

	for _, user := range Users {
		if user.Name == name {
			fmt.Printf("User with name '%s' already exists.\n", name)
			return
		}
	}

	user := models.User{
		ID:   currentID,
		Name: name,
		Role: models.NormalUser,
	}
	currentID++
	Users = append(Users, user)

	fmt.Printf("User created: %s\n", user.Name)
	UserFunctionality(user)
	fmt.Println("--------------------------------------")
}

func UserLogin() {

	fmt.Println(" LOGIN  ")
	fmt.Print("Enter your name: ")
	var name string
	fmt.Scan(&name)

	// if name == MainUser.Name {
	// 	fmt.Printf("Admin user '%s' logged in.\n", MainUser.Name)
	// 	UserFunctionality(MainUser)
	// 	return
	// }

	var loggedInUser models.User
	userFound := false
	for _, user := range Users {
		if user.Name == name {
			loggedInUser = user
			userFound = true
			break
		}
	}

	if !userFound {
		fmt.Println("User not found. Please register first.")
		return
	}

	fmt.Printf("User logged in: %s\n", loggedInUser.Name)
	UserFunctionality(loggedInUser)
}

func UserFunctionality(user models.User) {

	fmt.Printf("Welcome %s, your user ID is %d.\n", user.Name, user.ID)
	fmt.Println("--------------------------------------")
	for {
		fmt.Println("Choose an option:")
		fmt.Println("1. View Profile")
		fmt.Println("2. Functionalities")
		fmt.Println("3. Log out")
		fmt.Println("Press 0 to Exit")

		var choice int
		fmt.Print("Enter your choice: ")
		fmt.Scan(&choice)

		switch choice {
		case 1:
			fmt.Println("--------------------------------------")
			fmt.Printf("Profile:\nID: %d\nName: %s \n", user.ID, user.Name)
			fmt.Print("Role :")
			switch user.Role {
			case 0:
				fmt.Println("Admin")
			case 1:
				fmt.Println("User")
			default:
				fmt.Println("Unknown Role")
			}
			fmt.Println("--------------------------------------")
		case 2:
			switch user.Role {
			case 0:
				fmt.Println("--------------------------------------")
				AdminFunc()
				fmt.Println("--------------------------------------")
			case 1:
				fmt.Println("--------------------------------------")
				UserFunc(user)
				fmt.Println("--------------------------------------")
			default:
				fmt.Println("Unknown Role")

			}
		case 3:
			fmt.Println("--------------------------------------")
			fmt.Println("Logging out...")
			fmt.Println("--------------------------------------")
			UserService()
		case 0:
			fmt.Println("Exiting...")
			return
		default:
			fmt.Println("Invalid choice. Please try again.")
		}
	}
}
