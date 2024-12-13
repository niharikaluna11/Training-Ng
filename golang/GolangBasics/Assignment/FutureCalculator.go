package Assignment

import (
	"fmt"
	"math"
)

//Start balance	Deposit	Interest	End balance
//1	$1,100.00	$100.00	$66.00	$1,166.00

// FutureValue calculates the future value based on PV, PMT, rate, and periods.
func FutureValue(pv, pmt, rate float64, periods int) float64 {
	rate = rate / 100
	fvPMT := pmt * (math.Pow(1+rate, float64(periods)) - 1) * (1 + rate) / rate
	fvPV := pv * math.Pow(1+rate, float64(periods))
	return fvPV + fvPMT
}

// DisplayDetails prints detailed calculations for each period.
func DisplayDetails(pv, pmt, rate float64, periods int) {
	rate = rate / 100
	totalDeposits := 0.0
	totalInterest := 0.0

	fmt.Println("Period\tStarting Balance\tDeposit\t\tInterest\tEnd Balance")

	for i := 1; i <= periods; i++ {
		interest := pv * rate
		endBalance := pv + interest + pmt

		fmt.Printf("%d\t$%.2f\t\t$%.2f\t\t$%.2f\t\t$%.2f\n", i, pv, pmt, interest, endBalance)

		totalDeposits += pmt
		totalInterest += interest
		pv = endBalance
	}

	fmt.Printf("\nTotal Deposits: $%.2f\n", totalDeposits)
	fmt.Printf("Total Interest: $%.2f\n", totalInterest)
	fmt.Printf("Final Balance: $%.2f\n", pv)
}

// FutureCalculator handles user input and calculates the future value.
func FutureCalculator() {
	var pv, pmt, rate float64
	var periods int

	fmt.Println("Future Value Calculator")
	fmt.Print("Enter Starting Amount (PV): ")
	fmt.Scan(&pv)

	fmt.Print("Enter Periodic Deposit (PMT): ")
	fmt.Scan(&pmt)

	fmt.Print("Enter Interest Rate per Period (I/Y) in %: ")
	fmt.Scan(&rate)

	fmt.Print("Enter Number of Periods (N): ")
	fmt.Scan(&periods)

	fv := FutureValue(pv, pmt, rate, periods)
	fmt.Printf("\nFuture Value: $%.2f\n\n", fv)

	// Display detailed period-by-period calculations
	DisplayDetails(pv, pmt, rate, periods)
}
