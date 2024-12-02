import axios from "axios";

// Service to deactivate a user
export const deactivateUser = async (username) => {
    const url = `http://localhost:5062/api/AdminRight/Deactivating?key=${username}`;

    const token = sessionStorage.getItem("token");

    if (!token) {
        throw new Error("Authorization token not found in sessionStorage.");
    }

    try {
        const response = await axios.delete(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            }, // Ensure the parameter matches server expectations
        });
        return response.data; // Return API response data
    } catch (error) {
        if (error.response) {
            console.error("Server error:", error.response.data);
        } else {
            console.error("Request failed:", error.message);
        }
        throw error; // Propagate the error for further handling
    }
};

// Service to reactivate a user
export const reactivateUser = async (key) => {
    const url = `http://localhost:5062/api/AdminRight/reactivate?key=${key}`;

    const token = sessionStorage.getItem("token");

    if (!token) {
        throw new Error("Authorization token not found in sessionStorage.");
    }

    try {
        const response = await axios.put(url, null, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        return response.data; // Return API response data
    } catch (error) {
        console.error("Error reactivating user:", error);
        throw error; // Propagate error to handle it in the caller function
    }
};
