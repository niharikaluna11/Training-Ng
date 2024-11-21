import axios from "axios";

// Forgot password service
export const forgotPassword = async (usernameOrEmail, otp, newPassword, confirmNewPassword) => {
    try {
        const response = await axios.post('http://localhost:5062/api/ForgotPassword/reset-password', {
            "usernameOrEmail": usernameOrEmail,
            "otp": otp,
            "newPassword": newPassword,
            "confirmNewPassword": confirmNewPassword
        });
        return response; // Returns the response from the API
    } catch (err) {
        return err; // In case of an error, return the error
    }
}

// Forgot password service (to send the OTP reset link)
export const sendResetLink = async (usernameOrEmail) => {
    try {
        // Send GET request to send the OTP
        const response = await axios.get(`http://localhost:5062/api/ForgotPassword/send-reset-link?UsernameorEmail=${usernameOrEmail}`);

        return response; // Returns the response from the API
    } catch (err) {
        return err; // In case of an error, return the error
    }
}
