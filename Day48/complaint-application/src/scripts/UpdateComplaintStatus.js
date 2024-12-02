import axios from "axios";

// Update complaint status
export const updateComplaintStatus = async (complaintId, organizationId, status, commentByUser, statusDate) => {
    try {
        // Retrieve the token from sessionStorage
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Prepare request body
        const requestBody = {
            complaintId,
            organizationId,
            status,
            commentByUser,
            statusDate,
        };

        // Sending PUT request to update complaint status
        const response = await axios.put(
            `http://localhost:5062/api/UpdateComplaint/UpdateComplaintStatus`,
            requestBody, // Send JSON data
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "application/json", // Ensure content type is JSON
                },
            }
        );

        return response.data; // Return response data (updated complaint status)
    } catch (err) {
        console.error("Error updating complaint status:", err);
        throw err; // Throw error for handling in caller
    }
};
