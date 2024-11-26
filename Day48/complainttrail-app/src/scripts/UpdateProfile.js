import axios from "axios";

// Update user profile
export const updateUserProfile = async (userId, formData) => {
    try {
        // Retrieve the token from sessionStorage
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Sending PUT request to update user profile
        const response = await axios.put(
            `http://localhost:5062/api/Profile/update-User-profile?userId=${userId}`,
            formData, // Send form data with file and text inputs
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "multipart/form-data", // Ensure content type is multipart
                },
            }
        );

        return response.data; // Return response data (updated user profile)
    } catch (err) {
        console.error("Error updating user profile:", err);
        throw err; // Throw error for handling in caller
    }
};


// Update organization profile
export const updateOrganizationProfile = async (userId, formData) => {
    try {
        // Retrieve the token from sessionStorage
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }

        // Sending PUT request to update organization profile
        const response = await axios.put(
            `http://localhost:5062/api/Profile/update-Organization-profile?userId=${userId}`,
            formData, // Send form data with file and text inputs
            {
                headers: {
                    Authorization: `Bearer ${token}`,
                    "Content-Type": "multipart/form-data", // Ensure content type is multipart
                },
            }
        );

        return response.data; // Return response data (updated organization profile)
    } catch (err) {
        console.error("Error updating organization profile:", err);
        throw err; // Throw error for handling in caller
    }
};
