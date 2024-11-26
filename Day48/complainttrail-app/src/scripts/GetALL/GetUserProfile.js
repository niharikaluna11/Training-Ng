import axios from "axios";

// Get user profile by user ID
export const getUserProfile = async (userId) => {
    try {
        // Retrieve the token from sessionStorage
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Sending GET request to retrieve user profile based on userId
        const response = await axios.get(
            `http://localhost:5062/api/Profile/Get-profile/?userId=${userId}`,
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            }
        );
        return response.data; // Returns the response containing the user profile data
    } catch (err) {
        console.error("Error fetching user profile:", err);
        throw err; // Throw error for handling in caller
    }
};
