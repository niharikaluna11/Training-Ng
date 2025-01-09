## User Input in Go

### Using bufio and os Packages
In Go, user input can be captured using the `bufio` and `os` packages. These packages allow you to read input from the console efficiently.

#### bufio Package
The `bufio` package provides buffered I/O. It helps in reading input line by line or character by character, making it suitable for scenarios where you need to process user input interactively.

#### os Package
The `os` package offers functions to interact with the operating system. It provides access to standard input, output, and error streams, among other functionalities. In this context, we use `os.Stdin` to capture input from the console.

### Capturing User Input
Here’s an example of capturing user input in Go:

```go
package main

import (
    "bufio"
    "fmt"
    "os"
)

func main() {
    fmt.Print("Enter your input: ")

    // Create a new reader
    reader := bufio.NewReader(os.Stdin)

    // Read input until a newline character
    input, _ := reader.ReadString('\n')

    // Print the captured input
    fmt.Println("Thank you! You entered:", input)

    // Print the type of the input
    fmt.Printf("Type of input: %T\n", input)
}
```

### Handling Numeric Input
The following example demonstrates how to process numeric input and handle potential conversion errors:

```go
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

    // Convert the input to a float
    numRating, err := strconv.ParseFloat(strings.TrimSpace(input), 64)

    if err != nil {
        fmt.Println("Error:", err)
        panic("Invalid numeric input")
    } else {
        fmt.Println("Added 1 to your input:", numRating+1)
    }
}
```

### Explanation of Additional Logic
1. **Importing Required Packages**:
   - `strconv`: For converting strings to numeric types.
   - `strings`: To manipulate and trim whitespace from input strings.

2. **Converting Input to Float**:
   - `strings.TrimSpace(input)`: Removes leading and trailing whitespace from the input.
   - `strconv.ParseFloat(input, 64)`: Converts the trimmed input string to a `float64`. If the conversion fails, an error is returned.

3. **Error Handling**:
   - If the conversion fails, an error message is printed, and the program terminates using `panic`.
   - If successful, the numeric input is processed (e.g., adding 1 to the input) and the result is displayed.

### Example Output
#### Valid Input
```
Enter your input: 5.5
Thank you! You entered: 5.5
Type of input: string
Added 1 to your input: 6.5
```
#### Invalid Input
```
Enter your input: hello
Error: strconv.ParseFloat: parsing "hello": invalid syntax
panic: Invalid numeric input
```

