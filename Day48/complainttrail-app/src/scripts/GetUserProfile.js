import axios from "axios";

// Get user profile by user ID
export const getUserProfile = async (userId) => {
    try {
        // Sending GET request to retrieve user profile based on userId
        const response = await axios.get(`http://localhost:5062/api/Profile/Get-profile/?userId=${userId}`);
        return response; // Returns the response containing the user profile data
    } catch (err) {
        return err; // In case of an error, return the error
    }
}
