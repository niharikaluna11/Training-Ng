import axios from "axios";

export const getDash = async () => {
    const url = `http://localhost:5062/api/AdminRight/GetDashboardSummary`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found.");
        }

        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            }
        });

        console.log("API Response:", response.data); // Log the full response for debugging

        // Assuming the response contains the necessary details
        return response.data; // return the response directly
    } catch (error) {
        console.error("Error fetching dashboard summary:", error);
        throw error; // Propagate the error
    }
};
