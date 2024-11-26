import axios from "axios";

export const forgotPassword = async (usernameOrEmail, otp, newPassword, confirmNewPassword) => {
    try {
        const response = await axios.post('http://localhost:5062/api/ForgotPassword/reset-password', {
            "usernameOrEmail": usernameOrEmail,
            "otp": otp,
            "newPassword": newPassword,
            "confirmNewPassword": confirmNewPassword
        });
        return response;
    } catch (err) {
        return err;
    }
}


export const sendResetLink = async (usernameOrEmail) => {
    try {
        const response = await axios.post(`http://localhost:5062/api/ForgotPassword/send-reset-link`, {
            usernameOrEmail
        });
        return response;
    } catch (err) {
        console.error("Error in sendResetLink service:", err);
        throw err; // Throw the error to handle it in the component
    }
};

