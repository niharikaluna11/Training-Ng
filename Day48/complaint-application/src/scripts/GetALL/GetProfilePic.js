import axios from "axios";

export const getProfilePic = async (username) => {
    try {

        const token = sessionStorage.getItem("token");

        if (!token) {
            throw new Error("Authorization token not found in sessionStorage.");
        }
        const response = await axios.get('http://localhost:5062/api/Profile/Get-profile-pic', {
            params: {
                username: username
            },
            headers: {
                Authorization: `Bearer ${token}`
            },
        });
        return response;
    } catch (err) {
        return err;
    }
};
