package main

import (
	"fmt"
	"io"
	"net/http"
)

func APIres() {
	fmt.Println("lco web request")

	// Perform the HTTP GET request
	response, err := http.Get(url)
	if err != nil {
		panic(err) // Handle errors appropriately in production code
	}

	// Ensure the connection is closed properly
	defer response.Body.Close()

	// Print the type of the response
	fmt.Printf("response is of type: %T\n", response)

	databytes, err := io.ReadAll(response.Body)
	if err != nil {
		panic(err)
	}
	data := string(databytes)
	fmt.Print(data)

}
