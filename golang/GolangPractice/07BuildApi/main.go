package main

import (
	"encoding/json"
	"fmt"
	"log"
	"math/rand"
	"net/http"
	"strconv"
	"time"

	"github.com/gorilla/mux"
)

// model for courses - file
type Course struct {
	CourseId    string  `json:"courseid"`
	CourseName  string  `json:"coursename"`
	CoursePrice int     `json:"price"`
	Author      *Author `json:"author"` //foreigh key refernce
}

type Author struct {
	Fullname string `json:"fullname"`
	Website  string `json:"website"`
}

// fake db
var courses []Course

// middleware helper
func (c *Course) IsEmpty() bool {
	//return c.CourseId == "" && c.CourseName == ""
	return c.CourseName == ""
}

func main() {
	//create update delete update courses
	//using slice as not using db


	fmt.Print("API- L)")

	r :=mux.NewRouter()
	courses=append(courses, Course{CourseId: "2",CourseName: "bjdb",
		CoursePrice: 278, Author: &Author{Fullname: "hids",Website: "hocn"},})

	r.HandleFunc("/",serveHome).Methods("GET")

	//listen to port
	log.Fatal(http.ListenAndServe(":4000",r))
	

}

//controllers - file

// server home route
// writer w reader r
func serveHome(w http.ResponseWriter, r *http.Request) {
	w.Write([]byte("<h1>welcome to api</h1>"))

}

func getAllCOurses(w http.ResponseWriter, r *http.Request) {
	fmt.Print("Get all courses")
	w.Header().Set("Content-Type", "application/json")
	json.NewEncoder(w).Encode(courses)

}

func getOneCourse(w http.ResponseWriter, r *http.Request) {
	//get id and check id exist and return the response
	fmt.Print("get one course")
	w.Header().Set("Content-Type", "application/json")

	//grab id from request
	params := mux.Vars(r)

	//loop through courses , find mactching id and return response
	for _, course := range courses {
		if course.CourseId == params["id"] {
			json.NewEncoder(w).Encode(course)
			return
		}
	}
	json.NewEncoder(w).Encode("no course for given id")
	return
}

// crud operation
func createOneCourse(w http.ResponseWriter, r *http.Request) {
	fmt.Print("create all courses")
	w.Header().Set("Content-Type", "application/json")

	//what if body is empty

	if r.Body == nil {
		json.NewEncoder(w).Encode("please send some data")
	}

	// {} course data
	var course Course
	_ = json.NewDecoder((r.Body)).Decode(&course)
	if course.IsEmpty() {
		json.NewEncoder(w).Encode("no data inside json")
		return
	}

	//generate uniue id nd convert it to string

	rand.Seed(time.Now().UnixNano())
	course.CourseId = strconv.Itoa(rand.Intn(100))
	courses = append(courses, course)

	json.NewEncoder(w).Encode(course)
	return

}
