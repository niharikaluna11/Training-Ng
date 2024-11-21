import axios from "axios"

export const login = async (username, password) => {
    try {
        const response = await axios.post('http://localhost:5062/api/User/LoginOFUser',
            {
                "usernameOrEmail": username,
                "password": password
            });
        return response;
    }
    catch (err) {
        return err;
    }
}