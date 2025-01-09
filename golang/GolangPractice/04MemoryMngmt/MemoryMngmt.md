# Memory Allocation in Go

## Overview
Memory allocation in Go is simple and managed automatically by the runtime. You don't need to explicitly allocate or free memory as in some other programming languages. The Go runtime handles it for you.

### `new()`
- **Purpose**: Allocates memory but does not initialize it.
- **Returns**: A pointer to the allocated memory, which is zeroed.
- **Usage Example**:
  ```go
  var p *int
  p = new(int) // p now points to a zeroed int
  fmt.Println(*p) // Output: 0
  ```

### `make()`
- **Purpose**: Allocates and initializes memory. Specifically used for slices, maps, and channels.
- **Returns**: The initialized value (not a pointer).
- **Usage Example**:
  ```go
  s := make([]int, 5) // Creates a slice of length 5
  fmt.Println(s) // Output: [0 0 0 0 0]
  ```

---

# Pointers in Go

## Overview
A pointer is a variable that stores the memory address of another variable.

### Declaring and Using Pointers
```go
var x int = 42
var p *int = &x // p points to x
fmt.Println(*p) // Dereference p to get the value of x (Output: 42)
```

### Key Points
- The `&` operator is used to get the address of a variable.
- The `*` operator is used to dereference a pointer (access the value stored at the memory address).

### Modifying Values via Pointers
```go
var x int = 42
var p *int = &x
*p = 21 // Changes the value of x through the pointer
fmt.Println(x) // Output: 21
```

---

# Arrays in Go

## Overview
An array is a fixed-length sequence of elements of the same type.

### Declaring and Initializing Arrays
```go
var arr [3]int // Declares an array of 3 integers, initialized to zero
arr[0] = 10
fmt.Println(arr) // Output: [10 0 0]
```

### Shortcut Initialization
```go
arr := [3]int{1, 2, 3} // Initializes an array with values
fmt.Println(arr) // Output: [1 2 3]
```

### Key Characteristics
- Arrays have a fixed size, defined at compile time.
- Access elements using an index starting from `0`.
- Array length can be obtained using `len()`.

### Iterating Over Arrays
```go
arr := [3]int{1, 2, 3}
for i, v := range arr {
    fmt.Printf("Index: %d, Value: %d\n", i, v)
}
```

---

This document provides a basic overview of memory allocation using `new()` and `make()`, pointers, and arrays in Go. Expand your knowledge further by exploring slices and maps, which are more commonly used in Go programming.

