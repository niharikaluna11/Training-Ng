
import axios from "axios";


export const createCategory = async (categoryData) => {
    const url = `http://localhost:5062/api/ComplaintCategory/CreateCategory`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }
        const response = await axios.post(url, categoryData, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        return response.data;
    } catch (error) {
        throw new Error("Error creating category: " + error.message);
    }
};

export const fetchCategories = async (pageNum = 1, pageSize = 9) => {
    const url = `http://localhost:5062/api/ComplaintCategory/GetAllCategories?pagenum=${pageNum}&pagesize=${pageSize}`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }
        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        console.log(response.data);
        return response.data.$values;
    } catch (error) {
        throw new Error("Error fetching categories: " + error.message);
    }
};


export const fetchCategoriesName = async (pageNum = 1, pageSize = 9) => {
    const url = `http://localhost:5062/api/ComplaintCategory/GetAllCategories?pagenum=${pageNum}&pagesize=${pageSize}`;

    try {
        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }
        const response = await axios.get(url, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });
        // Extract only the category names from the response data
        const categories = response.data.$values.map(category => category.name);
        return categories; // Return only the names
    } catch (error) {
        throw new Error("Error fetching categories: " + error.message);
    }
};
