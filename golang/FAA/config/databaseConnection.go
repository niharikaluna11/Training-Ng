package config

import (
	"fmt"
	"myModule/model"

	// "gorm.io/driver/sqlserver"
	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	return fmt.Sprintf("root:niharika1102@tcp(127.0.0.1:3306)/niharika?charset=utf8mb4&parseTime=True&loc=Local")
}

func C`Installation`
go get -u github.com/gin-gonic/gin
go get -u gorm.io/gorm
go get -u gorm.io/driver/mysql
go get -u go.uber.org/zap
go get github.com/joho/godotenv
go get -u golang.org/x/crypto/bcrypt 
go get github.com/dgrijalva/jwt-go

`Running the server`
go run .onnectDB() *gorm.DB {
	userdb, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	userdb.AutoMigrate(&model.User{}, &model.Flights{})
	return userdb
}

