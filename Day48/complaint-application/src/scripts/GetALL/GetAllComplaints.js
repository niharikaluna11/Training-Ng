import axios from "axios";
export const getAllComplaints = async (page, pageSize) => {
    const url = `http://localhost:5062/api/Complaint/GetComplaints?orgId=1&pagenum=${page}&pagesize=${pageSize}`;

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

        console.log("API Response:", response.data); // Log the full response

        if (response.data && response.data.complaints && response.data.complaints.$values) {
            return {
                complaints: response.data.complaints.$values,
                totalCount: response.data.totalCount
            };
        } else {
            throw new Error("No complaints data found.");
        }

    } catch (error) {
        console.error("Error fetching complaints:", error); // Log the error for debugging
        throw error; // Propagate error to be handled by the caller
    }
};




export const getComplaintDetails = async (complaintId) => {
    const url = `http://localhost:5062/api/Complaint/GetComplaintDetails?complaintid=${complaintId}`;

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

        console.log("API Response:", response.data); // Log the full response

        // Assuming response.data is the complaint data directly (not inside another property)
        if (response.data) {
            return {
                complaintDetails: response.data,  // Directly returning the complaint details
            };
        } else {
            throw new Error("No complaint details found.");
        }

    } catch (error) {
        console.error("Error fetching complaint details:", error); // Log the error for debugging
        throw error; // Propagate error to be handled by the caller
    }
};
