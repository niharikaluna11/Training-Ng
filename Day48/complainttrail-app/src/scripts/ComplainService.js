import axios from './myAxiosInterceptor';

export function getComplaintCategories() {
  return axios.get('http://localhost:5062/api/ComplaintCategory/GetAllCategories');
}