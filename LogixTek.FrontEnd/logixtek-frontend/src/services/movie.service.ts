import axios from "axios";
import authHeader from "./auth-header";

const API_URL = "https://localhost:7278/api/Movie/";

const getUserId = () => {
    const userStr  = localStorage.getItem("user");
    let user = null;
    if (userStr)
    user = JSON.parse(userStr);
    return user?.id
}

export const getAll = (userId: any) => {
  return axios.get(API_URL + "getall/" + userId ?? getUserId(), { headers: authHeader() });
};

  export const dislike = () => {
    let userId = getUserId()
    return axios.post(API_URL + "dislike/" + userId, { headers: authHeader(),  'Content-Type':'application/json'});
  };
  
  export const like = () => {
    let userId = getUserId()
    return axios.post(API_URL + "like/" + userId, { headers: authHeader(),  'Content-Type':'application/json' });
  };
