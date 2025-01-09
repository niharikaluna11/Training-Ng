package config

import (
	"fmt"
	"mymodule/model"

	"gorm.io/driver/mysql"
	"gorm.io/gorm"
)

func DatabaseDsn() string {
	return fmt.Sprintf("root:niharika1102@tcp(127.0.0.1:3306)/niharika?charset=utf8mb4&parseTime=True&loc=Local")
}

func ConnectDB() *gorm.DB {
	userdb, err := gorm.Open(mysql.Open(DatabaseDsn()), &gorm.Config{})

	if err != nil {
		panic("Failed to connect DB")
	}
	userdb.AutoMigrate(&model.User{}, &model.Flights{})
	return userdb
}
