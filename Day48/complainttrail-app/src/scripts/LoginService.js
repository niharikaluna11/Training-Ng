import axios from 'axios';

export function login(username, password) {
  return axios.post('http://localhost:5062/api/User/LoginOFUser', {
    "usernameOrEmail": username,
    "password": password
  });
}