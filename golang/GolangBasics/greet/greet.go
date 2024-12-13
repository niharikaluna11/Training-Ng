package greet

import (
	"fmt"
)

    
func ng() int {
    var a int = 5
    return a
}


func GreetEveryone() {
	// fmt.Println("Greeting to you brooo")

	// fmt.Println("Hello, World!")              // Prints with a newline
    // fmt.Print("This is Go programming. ")     // Prints without a newline
    // fmt.Printf("I am %d years old.\n", 25)    // Formatted printing


    Ng := func() int {
        var a int = 5
        return a
    }

    fmt.Println("Greeting to you, brooo")
    fmt.Println(Ng())

    fmt.Println(ng())
    triple := makeMultiplier(3)
	fmt.Println(triple(5))
	// name := "ng"
    // age := 21
    // message := fmt.Sprintf("My name is %s and I am %d years old.", name, age)
    // fmt.Println(message)

}
// A function that returns another function
func makeMultiplier(factor int) func(int) int {
    // Returning a function that multiplies its input by the given factor
    return func(input int) int {
        return input * factor
    }
}
