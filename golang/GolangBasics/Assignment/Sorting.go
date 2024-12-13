package Assignment

import (
	"fmt"
	"strings"
)

func isSorted(array []int) bool {
	for i := 0; i < len(array)-1; i++ {
		if array[i] > array[i+1] {
			return false
		}
	}
	return true
}

// time complexity Best Case: 𝑂(𝑛), worst or average: O(n^2)
// Space: 𝑂(1)
func bubbleSort(array []int) {
	fmt.Println("Unsorted array:", array)
	n := len(array)
	if isSorted(array) {
		fmt.Println("Array is already sorted.")
		return
	}

	for outer := 0; outer < n-1; outer++ {
		for inner := 0; inner < n-outer-1; inner++ {
			if array[inner] > array[inner+1] {
				array[inner], array[inner+1] = array[inner+1], array[inner]
			}
		}
	}

	fmt.Println("Using Bubble Sort")
	fmt.Println("Sorted array using Bubble Sort:", array)
}

// time complexity Best, worst, or average: O(n^2)
// Space: 𝑂(1)
func selectionSort(array []int) {
	fmt.Println("Unsorted array:", array)
	n := len(array)
	if isSorted(array) {
		fmt.Println("Array is already sorted.")
		return
	}

	for outer := 0; outer < n-1; outer++ {
		minIndex := outer
		for inner := outer + 1; inner < n; inner++ {
			if array[inner] < array[minIndex] {
				minIndex = inner
			}
		}
		array[outer], array[minIndex] = array[minIndex], array[outer]
	}

	fmt.Println("Using Selection Sort")
	fmt.Println("Sorted array using Selection Sort:", array)
}

// time complexity Best Case: 𝑂(𝑛), worst or average: O(n^2)
// Space: 𝑂(1)
func insertionSort(array []int) {
	fmt.Println("Unsorted array:", array)
	n := len(array)
	if isSorted(array) {
		fmt.Println("Array is already sorted.")
		return
	}

	for outer := 1; outer < n; outer++ {
		key := array[outer]
		inner := outer - 1
		for inner >= 0 && array[inner] > key {
			array[inner+1] = array[inner]
			inner--
		}
		array[inner+1] = key
	}

	fmt.Println("Using Insertion Sort")
	fmt.Println("Sorted array using Insertion Sort:", array)
}

func Sorting() {
	for {
		fmt.Println("\nSelect an option:")
		fmt.Println("1) Implement Bubble Sort")
		fmt.Println("2) Implement Selection Sort")
		fmt.Println("3) Implement Insertion Sort")
		fmt.Println("0) Quit")
		fmt.Print("Enter your choice: ")

		var choice int
		fmt.Scan(&choice)

		fmt.Print("Enter the array to sort (comma-separated): ")
		var input string
		fmt.Scan(&input)

		array := parseInput(input)
		switch choice {
		case 1:
			bubbleSort(array)
		case 2:
			selectionSort(array)
		case 3:
			insertionSort(array)
		case 0:
			fmt.Println("Thank you! Exiting...")
			return
		default:
			fmt.Println("Invalid choice. Please try again.")
		}
	}
}

func parseInput(input string) []int {
	var array []int
	for _, value := range strings.Split(input, ",") {
		var number int
		fmt.Sscanf(strings.TrimSpace(value), "%d", &number)
		array = append(array, number)
	}
	return array
}
