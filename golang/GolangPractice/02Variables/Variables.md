
### Variables in Go

#### Declaring Variables
To use a variable in Go, it must be declared and initialized (or left with a default value). If a declared variable is not used, the compiler will throw an error.

#### String Example
```go
package main
import "fmt"

func main() {
    var un string = "some"
    fmt.Println(un) // Prints the variable value and moves to a new line
    fmt.Printf("Variable is of type %T\n", un) // Prints the type of the variable
}
```
- **%T**: A format specifier to print the type of a variable.

#### Boolean Example
```go
package main
import "fmt"

func main() {
    var isLogging bool = false
    fmt.Println(isLogging) // Prints: false
}
```

#### Float Example
Go supports both `float32` and `float64` types for floating-point numbers.

#### Default Values
When a variable is declared without initialization, Go assigns a default value:
- Numbers: `0`
- Strings: `""` (empty string)
- Booleans: `false`

#### Type Inference
You can let Go infer the type of a variable based on its value:
```go
package main
import "fmt"

func main() {
    var website = "learncode.in"
    fmt.Println(website) // Prints: learncode.in
}
```
Once a type is inferred, it cannot be changed.

#### Short Declaration
Go provides a shorthand for variable declaration and initialization:
```go
package main
import "fmt"

func main() {
    numOfUsers := 200
    fmt.Println(numOfUsers) // Prints: 200
}
```
- Note: This shorthand cannot be used for global variables.

#### Global and Constant Variables
Global variables must be declared using the `var` keyword. Short declaration (`:=`) is not allowed for global scope.
```go
package main
import "fmt"

const Login string = "nbedvc" // Public constant variable

func main() {
    var jwtToken int = 300 // Global variable
    fmt.Println(Login)
    fmt.Println(jwtToken)
}
```

