import axios from 'axios';

export function Register(fname, lname,username, password, email, date, role, type) {
  return axios.post('http://localhost:5062/api/User/RegistrationOFUser', {
    "fname": fname,
    "lname": lname,
    "username": username,
    "password": password,
    "email": email,
    "dateOfBirth": date,
    "role": role, 
    "types": type  
  });
}
