import axios from "axios";

// Get a complaint by ID
export const getComplaintById = async (id) => {
  const url = `http://localhost:5062/api/Complaint/GetComplaintById?id=${id}`;

  const token = sessionStorage.getItem("token");

  if (!token) {
    throw new Error("Authorization token not found in sessionStorage.");
  }


  try {
    // Make the API request with authentication
    const response = await axios.get(url, {
      headers: {
        Authorization: `Bearer ${token}`, // Replace with your actual token
      },
    });

    return response.data; // Return the complaint data
  } catch (error) {
    console.error(`Error fetching complaint with ID ${id}:`, error);
    throw error; // Propagate the error
  }
};
