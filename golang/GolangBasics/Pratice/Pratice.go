package pratice

import (
	"fmt"
)


func IfPalindrome(str string) bool {
	var reversed string
	for i := len(str) - 1; i >= 0; i--{
		reversed += string(str[i]) 
	}
	return str == reversed 
}

func Palindrome(){
	str :="aaa"
	// str="civic"
	// str="disha"
	if IfPalindrome(str) {
		fmt.Println(str, "is a palindrome.")
	} else {
		fmt.Println(str, "is not a palindrome.")
	}
}

func CheckBits(num int) int{
	count := 0
	for num > 0 {
		if num&1 == 1 {  //bitwise AND operation
			count++
		}
		num >>= 1 // right shift
	}
	return count
}