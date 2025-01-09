package main

import (
	"bytes"
	"encoding/json"
	"log"
	"myModule/model"
	"net/http"
	"net/http/httptest"
	"testing"

	"github.com/DATA-DOG/go-sqlmock"
	"github.com/gin-gonic/gin"
	"github.com/stretchr/testify/assert"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

// Setup Router with the AddUser route
func setupRouter(db *gorm.DB) *gin.Engine {
	gin.SetMode(gin.TestMode)
	r := gin.Default()
	flightDbConnector = db // Initialize flightDbConnector before using it in AddUser
	r.POST("/save-user", AddUser)
	return r
}

func NewMockDB() (*gorm.DB, sqlmock.Sqlmock) {
	db, mock, err := sqlmock.New()
	if err != nil {
		log.Fatalf("An error '%s' was not expected when opening a stub database connection", err)
	}

	gormDB, err := gorm.Open(mysql.New(mysql.Config{
		Conn:                      db,
		SkipInitializeWithVersion: true,
	}), &gorm.Config{})

	if err != nil {
		log.Fatalf("An error '%s' was not expected when opening gorm database", err)
	}

	return gormDB, mock
}

func TestAddUser(t *testing.T) {
	// Initialize mock database
	db, mock := NewMockDB()

	// Set up the router with the mock database
	r := setupRouter(db)

	t.Run("Valid user registration", func(t *testing.T) {
		// Sample valid user data
		user := model.User{
			Name:     "John Doe",
			Email:    "john.doe@example.com",
			Password: "password123",
			Phone:    "1234567890",
			Address:  "123 Main St",
			City:     "Anytown",
		}

		// Marshal user struct into JSON
		userJSON, _ := json.Marshal(user)

		// Create the POST request to send to the server
		req, _ := http.NewRequest(http.MethodPost, "/save-user", bytes.NewBuffer(userJSON))
		req.Header.Set("Content-Type", "application/json")

		// Create the response recorder to capture the response
		w := httptest.NewRecorder()

		// Mock SQL queries
		// Use regular expression to match the SELECT query
		mock.ExpectQuery("^SELECT (.+) FROM `users` WHERE email = ? AND `users`.`deleted_at` IS NULL ORDER BY `users`.`id` LIMIT ?$").
			WithArgs(user.Email, 1).             // Ensure both the email and the limit (typically `1`).
			WillReturnRows(sqlmock.NewRows(nil)) // No rows returned

		// Simulate a successful insertion of the new user
		mock.ExpectBegin() // Transaction begins
		mock.ExpectExec("INSERT INTO `users`").
			WithArgs(sqlmock.AnyArg(), sqlmock.AnyArg(), sqlmock.AnyArg(), sqlmock.AnyArg(), sqlmock.AnyArg(), sqlmock.AnyArg(), sqlmock.AnyArg()).
			WillReturnResult(sqlmock.NewResult(1, 1)) // Insert was successful
		mock.ExpectCommit() // Transaction commits

		// Perform the request
		r.ServeHTTP(w, req)

		// Assert response code and message
		assert.Equal(t, http.StatusCreated, w.Code)                      // Expected status is 201 Created
		assert.Contains(t, w.Body.String(), "User created successfully") // Ensure success message is returned

		// Debugging to check if the expectations were met
		err := mock.ExpectationsWereMet()
		if err != nil {
			t.Fatalf("There were unmet expectations: %v", err)
		}
	})
}
