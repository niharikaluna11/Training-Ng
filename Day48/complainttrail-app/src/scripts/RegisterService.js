import axios from 'axios';
import { toast } from 'vue3-toastify';
import 'vue3-toastify/dist/index.css';

export async function Register(fname, lname, username, password, email, date, role, type) {
  try {
    const response = await axios.post('http://localhost:5062/api/User/RegistrationOFUser', {
      fname,
      lname,
      username,
      password,
      email,
      dateOfBirth: date,
      role: +role,   // Ensure role is a number
      types: +type   // Ensure type is a number
    });

    return response;  // Return the response from the API
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
