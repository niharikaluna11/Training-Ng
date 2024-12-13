package Assignment

import (
	"fmt"
)

type Account struct {
	Name    string
	Balance float64
}


type Bank struct {
	Account Account
}


func NewBank(name string) *Bank {
	return &Bank{
		Account: Account{Name: name, Balance: 0.0},
	}
}

func (b *Bank) Deposit(amount float64) {
	if amount <= 0 {
		fmt.Println("Deposit amount must be greater than zero.")
		return
	}
	b.Account.Balance += amount
	fmt.Printf("Successfully deposited $%.2f into %s's account.\n", amount, b.Account.Name)
}

func (b *Bank) Withdraw(amount float64) {
	if amount <= 0 {
		fmt.Println("Withdrawal amount must be greater than zero.")
		return
	}
	if amount > b.Account.Balance {
		fmt.Println("Insufficient funds.")
		return
	}
	b.Account.Balance -= amount
	fmt.Printf("Successfully withdrew $%.2f from %s's account.\n", amount, b.Account.Name)
}


func (b *Bank) CheckBalance() {
	fmt.Printf("%s's account balance: $%.2f\n", b.Account.Name, b.Account.Balance)
}


func BankF() {
	var accountName string
	fmt.Print("Enter account holder's name: ")
	fmt.Scan(&accountName)

	bank := NewBank(accountName)

	for {
		fmt.Println("\nSimple Banking Operations:")
		fmt.Println("1. Deposit")
		fmt.Println("2. Withdraw")
		fmt.Println("3. Check Balance")
		fmt.Println("4. Exit")

		fmt.Print("Choose an option: ")
		var choice int
		fmt.Scan(&choice)

		switch choice {
		case 1:
			var amount float64
			fmt.Print("Enter amount to deposit: ")
			fmt.Scan(&amount)
			bank.Deposit(amount)

		case 2:
			var amount float64
			fmt.Print("Enter amount to withdraw: ")
			fmt.Scan(&amount)
			bank.Withdraw(amount)

		case 3:
			bank.CheckBalance()

		case 4:
			fmt.Println("Thank you. !")
			return

		default:
			fmt.Println("Invalid option. Please try again.")
		}
	}
}
