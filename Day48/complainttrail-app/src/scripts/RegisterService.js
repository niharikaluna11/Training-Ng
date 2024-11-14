import axios from 'axios';

export function Register(name,username, password,email,date) {
  return axios.post('http://localhost:5062/api/User/RegistrationOFUser', {
  "name": name,
  "username": username,
  "password": password,
  "email": email,
  "dateOfBirth": date,
  "role": 0,
  "types": 1
  });
}