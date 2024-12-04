import axios from "axios";




export const getOrgIdByUserId = async (userid) => {
    const url = `http://localhost:5062/api/Organization/GetOrgByUserID?userId=${userid}`;

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

        if (response) {
            return response;
        } else {
            throw new Error("No complaints data found.");
        }

    } catch (error) {
        console.error("Error fetching complaints:", error); // Log the error for debugging
        throw error; // Propagate error to be handled by the caller
    }
};


export const getAllComplaintsbyorg = async (orgid, page, pageSize) => {
    const url = `http://localhost:5062/api/Complaint/GetAllComplaintsByOrgId?orgId=${orgid}&pageNum=${page}&pageSize=${pageSize}`;

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



export const getAllComplaintsbyuser = async (uid, page, pageSize) => {
    const url = `http://localhost:5062/api/Complaint/GetAllComplaintsByUserId?userid=${uid}&pageNum=${page}&pageSize=${pageSize}`;

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



export const getAllComplaintsbycategory = async (cid, page, pageSize) => {
    const url = `http://localhost:5062/api/Complaint/GetAllComplaintsByCategoryId?categoryid=${cid}&pageNum=${page}&pageSize=${pageSize}`;

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
