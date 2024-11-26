import axios from "axios";

// Get all users' profiles
export const getAllUsersProfiles = async () => {
    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Make the GET request with Authorization header
        const response = await axios.get('http://localhost:5062/api/Profile/Get-All-users-profile', {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        // Return the data part of the response
        return response.data; // Assuming the data returned is an array of user profiles
    } catch (err) {
        console.error("Error fetching users' profiles:", err);
        throw err; // Rethrow the error for handling in the caller
    }
};

// Get all organizations' profiles
export const getAllOrganizationsProfiles = async () => {
    try {
        // Retrieve the token from sessionStorage
        const token = sessionStorage.getItem("token");

        // Check if the token exists
        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Make the GET request with Authorization header
        const response = await axios.get(
            `http://localhost:5062/api/Profile/Get-All-organizations-profile`,
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );

        // Return the data part of the response (assuming the response contains organization profiles)
        return response.data;
    } catch (err) {
        console.error("Error fetching organizations' profiles:", err);
        throw err; // Throw error for handling in the caller
    }
};