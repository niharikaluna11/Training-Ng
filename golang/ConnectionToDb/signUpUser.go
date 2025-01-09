package main

import (
	"auth-micro/model"
	"fmt"
	"net/http"

	"github.com/gin-gonic/gin"
	"go.uber.org/zap"
	"golang.org/x/crypto/bcrypt"
	"gorm.io/gorm"
)

func validateUser(user model.User) error {
	// Email format validation
	// if !isValidEmail(user.Email) {
	// 	return fmt.Errorf("Invalid email format")
	// }

	// Password length validation
	if len(user.Password) > 8 {
		return fmt.Errorf("Password must be at least 8 characters long")
	}

	// Phone number format validation (assuming it should be numeric)
	// if !isValidPhone(user.Phone) {
	// 	return fmt.Errorf("Invalid phone number format")
	// }

	// Address and City check
	// if user.Address == "" || user.City == "" {
	// 	return fmt.Errorf("Address and City are required")
	// }

	return nil
}

// Helper function to validate email format using regex
// func isValidEmail(email string) bool {
// 	re := regexp.MustCompile(`^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,}$`)
// 	return re.MatchString(email)
// }

// Helper function to validate phone number format
// func isValidPhone(phone string) bool {
// 	re := regexp.MustCompile(`^\d{10}$`)
// 	return re.MatchString(phone)
// }

func AddUser(ctx *gin.Context) {
	var user model.User
	ctx.ShouldBindJSON(&user)

	logger.Info("Recieved User Request", zap.String("useremail", user.Email), zap.String("username", user.Name))

	// ?1. Write the validation Logic

	if err := validateUser(user); err != nil {
		ctx.JSON(http.StatusBadRequest, gin.H{"message": err.Error()})
		return
	}

	// ?2. Check for Existing user.
	var existingUser model.User

	userNotFoundError := userDbConnector.Where("email = ?", user.Email).First(&existingUser).Error

	if userNotFoundError == gorm.ErrRecordNotFound {
		// ?3. hash password
		hashedPassword, err := bcrypt.GenerateFromPassword([]byte(user.Password), bcrypt.DefaultCost)

		if err != nil {
			fmt.Println(err)
		}
		fmt.Println(hashedPassword)

		newUser := &model.User{Name: user.Name, Email: user.Email, Password: string(hashedPassword), Address: user.Address, City: user.City, Phone: user.Phone}

		primaryKey := userDbConnector.Create(newUser)

		if primaryKey.Error != nil {
			logger.Error("Failed to Create user", zap.String("userPhone ", user.Phone), zap.Error(primaryKey.Error))
			ctx.JSON(http.StatusConflict, gin.H{"message": "The Phone is already registered"})
			return
		}
		logger.Info(fmt.Sprintf("User %s created successfully", user.Name))
		ctx.JSON(http.StatusCreated, gin.H{"message": "User created successfully"})

	} else {
		logger.Warn("User Email Already Exist", zap.String("usermail", user.Email))
		ctx.JSON(http.StatusConflict, gin.H{"message": "User Email Already Exist"})
	}

}
