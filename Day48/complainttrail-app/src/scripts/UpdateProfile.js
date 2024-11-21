import axios from "axios";

// Update user profile by userId
export const updateUserProfile = async (userId, profileData) => {
    try {
        // Create a FormData object to handle multipart/form-data
        const formData = new FormData();

        // Append the fields to the FormData object
        formData.append("userId", userId);
        formData.append("FirstName", profileData.FirstName);
        formData.append("LastName", profileData.LastName);
        formData.append("ProfilePicture", profileData.ProfilePicture); // This should be a File object (image data)
        formData.append("Address", profileData.Address);
        formData.append("DateOfBirth", profileData.DateOfBirth); // Format: "YYYY-MM-DDT00:00:00Z"
        formData.append("Email", profileData.Email);
        formData.append("Phone", profileData.Phone);
        formData.append("Preferences", profileData.Preferences);

        // Sending PUT request to update the user profile
        const response = await axios.put(
            `http://localhost:5062/api/Profile/update-User-profile?userId=${userId}`,
            formData, {
            headers: {
                "Content-Type": "multipart/form-data", // Specify the content type for file uploads
            },
        }
        );
        return response; // Returns the response from the API
    } catch (err) {
        return err; // In case of an error, return the error
    }
}


export const updateOrganizationProfile = async (userId, profileData) => {
    try {
        // Create a FormData object to handle multipart/form-data
        const formData = new FormData();

        // Append the fields to the FormData object
        formData.append("userId", userId);
        formData.append("Name", profileData.Name);
        formData.append("ProfilePicture", profileData.ProfilePicture); // This should be a File object (image data)
        formData.append("Types", profileData.Types);
        formData.append("Address", profileData.Address);
        formData.append("Phone", profileData.Phone);
        formData.append("Email", profileData.Email);

        // Sending PUT request to update the organization profile
        const response = await axios.put(
            `http://localhost:5062/api/Profile/update-Organization-profile`,
            formData, {
            headers: {
                "Content-Type": "multipart/form-data", // Specify the content type for file uploads
            },
        }
        );
        return response; // Returns the response from the API
    } catch (err) {
        return err; // In case of an error, return the error
    }
}
