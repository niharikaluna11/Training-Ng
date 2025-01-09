package main

import (
	"bufio"
	"fmt"
	"os"
	"strconv"
	"strings"
)

func main() {
	fmt.Println("Enter your input: ")

	// Create a new reader
	reader := bufio.NewReader(os.Stdin)

	// Read input until a newline character
	input, _ := reader.ReadString('\n')

	// Print the captured input
	fmt.Println("Thank you! You entered:", input)

	// Print the type of the input
	fmt.Printf("Type of input: %T \n", input)

	numrating, err := strconv.ParseFloat(strings.TrimSpace(input), 64)

	if err != nil {
		fmt.Print(err)
		panic("errorrr")

	} else {
		fmt.Println("added 1 to r", numrating+1)
	}

}
