import axios from "axios";

export const submitQuery = async (email, query) => {
    try {

        // Create the request payload
        const requestBody = {
            email,
            query,
        };

        // Sending POST request to the SubmitQuery endpoint
        const response = await axios.post(
            "http://localhost:5062/api/UserHelp/SubmitQuery", // Replace with your actual API endpoint
            requestBody,
            {
                headers: { // Add token if available
                    "Content-Type": "application/json", // Ensure content type is JSON
                },
            }
        );

        return response.data; // Return the API response data
    } catch (err) {
        console.error("Error submitting query:", err);
        throw err; // Throw error for handling in the caller
    }
};
