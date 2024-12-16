# Flight Booking System
## Models
### User
- Represents a user of the system.
### Flight
- Attributes:
  - Source
  - Destination
  - Seats
  - Departure Time
  - Arrival Time
  - Price
  - IsAvailable (default: true)
  - IsGoing (default: scheduled)
### UserFlight
- Represents the relationship between a User and a Flight (1 User can book 1 Flight).
---
## Roles and Responsibilities
### Admin Role
- **Add Flights**
  - Provide flight details including:
    - Source
    - Destination
    - Seats
    - Departure Time
    - Arrival Time
    - Price
    - IsAvailable
    - IsGoing (default: scheduled)
- **View Flights**
  - Verify all added flights.
- **Delete Flights**
  - Soft delete flights that are not going.
### User Role
- **Search Flights**
  - Find flights between a specified source and destination.
- **View Flight Details**
  - View detailed information about a specific flight.
- **Book Flights**
  - Reserve a seat on a flight.
- **Sort Flights**
  - Sort search results by:
    - Price
    - Name (Airline).
---
## API Endpoints
### Admin Endpoints
#### Add a Flight
- **Endpoint:** `POST /admin/add-flight`
- **Description:** Allows the admin to add a new flight with the required details.
- **Parameters:**
  - Source
  - Destination
  - Seats
  - Departure Time
  - Arrival Time
  - Price
  - IsAvailable
  - IsGoing
#### View All Flights
- **Endpoint:** `GET /admin/view-flights`
- **Description:** Allows the admin to view all flights for verification purposes.
### User Endpoints
#### Search and Sort Flights
- **Endpoint:** `GET /flights?source={}&destination={}&sort={price|name}`
- **Description:** Allows users to search for flights between a source and destination and sort the results by price or name (airline).
---
## Features Summary
### Admin Features
1. Add flights with full details.
2. View all added flights.
3. Soft delete flights that are not going.
### User Features
1. Search for flights by source and destination.
2. View detailed flight information.
3. Book flights.
4. Sort flight search results by price or name.
---
## Notes
1. This system is a console-based application and requires user input for all operations.
2. Ensures robust validation for flight details, including a strict check to prevent past dates.