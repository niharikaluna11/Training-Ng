import axios from "axios";

// Get all complaints
export const getAllComplaints = async () => {
    const url = "http://localhost:5062/api/UpdateComplaint/GetComplaints?orgId=1";

    try {

        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Add authentication token if needed
        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`, // Replace with your actual token
            }
        });
        return response.data; // Returns the data from the API response
    } catch (error) {
        console.error("Error fetching complaints:", error);
        throw error; // Propagates the error to be handled by the caller
    }
};
