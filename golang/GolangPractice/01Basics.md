## Getting Started with Go
 go get -u github.com/gorilla/mux for api things is this?
 
### OOPS Concept
There is no inheritance in Go. NO Super & NO Parent Class.
GO is a simple language which says that inheritance make language complex.

### Initializing a Go Module
To create a new Go module, use the following command:
```bash
go mod init goapp
```
This initializes a new module named `goapp` and creates a `go.mod` file to manage dependencies.

### Main File in Go
The main file in a Go project is essential as it acts as the entry point of the program. By convention, this file is named `main.go`. The compiler looks for this file to start the execution of the program.

### Running a Go Program
You can run your Go program with these commands:
- `go run .` - Runs all files in the current directory.
- `go run my/cmd` - Runs the file(s) located in the specified subdirectory.

### Lexical Analysis in Go
Go includes a **lexer**, which ensures your code adheres to the language's grammar rules. The lexer:
1. Validates the syntax of your code.
2. Automatically inserts semicolons where required based on Go's formatting rules.

### Case Sensitivity in Go
Go is a case-sensitive language, and naming conventions play a significant role in determining the visibility of identifiers:
- **Public**: If an identifier starts with a capital letter (e.g., `ExportedVar`), it is accessible outside the package.
- **Private**: If an identifier starts with a lowercase letter (e.g., `internalVar`), it is accessible only within the same package.

### Data Types in Go
Go provides a range of basic and advanced data types:

#### Basic Types
- **string**: Represents textual data.
- **bool**: Represents boolean values (`true` or `false`).
- **int**: Represents integer values (e.g., `int8`, `int16`, `int32`, `int64`, or platform-dependent `int`).
- **float**: Represents floating-point numbers (e.g., `float32`, `float64`).
- **complex**: Represents complex numbers (e.g., `complex64`, `complex128`).

#### Advanced Types
- **Array**: A fixed-size collection of elements of the same type.
- **Slice**: A dynamically-sized, more flexible version of an array.
- **Map**: A key-value store, similar to dictionaries in other languages.
- **Struct**: A composite data type that groups fields under one type.
- **Pointer**: Allows you to reference memory addresses directly.




### Summary
Go is a powerful, case-sensitive programming language that emphasizes simplicity and efficiency. Its lexer ensures proper grammar usage and automatically manages certain formatting tasks. Understanding the core and advanced types in Go, along with naming conventions, is crucial for writing effective Go programs.

