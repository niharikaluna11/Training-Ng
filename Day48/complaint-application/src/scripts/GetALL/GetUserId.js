import axios from "axios";

// Get user ID by username
export const getUserIdByUsername = async (username) => {
    try {
        // Sending GET request with the username as a query parameter
        const response = await axios.get(
            `http://localhost:5062/api/User/GetUserIdByUsername?username=${username}`
        );
        console.log(response.data.userId);
        return response.data.userId;
        // Return the user ID from the response
    } catch (err) {
        console.error("Error fetching user ID:", err);
        throw err; // Throw error for handling in caller
    }
};
