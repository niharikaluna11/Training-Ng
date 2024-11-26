import axios from 'axios';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export async function Register(fname, lname, username, password, email, otp, date, role, type) {
  try {
    const response = await axios.post('http://localhost:5062/api/User/RegistrationOFUser', {
      fname,
      lname,
      username,
      password,
      email,
      otp,
      dateOfBirth: date,
      role: +role,
      types: +type
    });

    return response;
  } catch (error) {
    console.log(error);
    toast.error(
      `Registration failed: ${error.message || "An unexpected error occurred."}`,
      {
        rtl: true,
        limit: 2,
        position: toast.POSITION.TOP_RIGHT,
      }
    );
    throw error; // Re-throw the error to allow further handling
  }
}


export async function SendOtp(email) {
  try {
    console.log("Sending OTP for email:", email);
    const response = await axios.post(`http://localhost:5062/api/User/send-otp`, { email });
    return response;
  } catch (error) {
    console.error(error);
    toast.error(
      `OTP request failed: ${error.response?.data?.message || "An unexpected error occurred."}`,
      {
        rtl: true,
        limit: 2,
        position: toast.POSITION.TOP_RIGHT,
      }
    );
    throw error; // Rethrow to allow further handling if necessary
  }
}

