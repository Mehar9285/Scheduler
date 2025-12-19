import axios from "axios";

const API_URL = "https://localhost:7063";

export const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Accept": "application/json",
    "Content-Type": "application/json"
  },
  withCredentials: true 
});


export const getTodaySchedule = async () => (await api.get("/schedule/today")).data;
export const getWeekSchedule = async () => (await api.get("/schedule/week")).data;


export async function addEvent(eventData) {
  const { date, name, contentType, startTime } = eventData;

  console.log("Sending Event Data:", { date, name, contentType, startTime });

  
  if (!name || !contentType || !date || !startTime) {
    console.error("Missing required fields in addEvent:", eventData);
    throw new Error("Missing fields");
  }

    return (await api.post("/schedule/add", {
    date,
    name,
    contentType,
    startTime
  })).data;
}
  /*return axios.post(`${API_URL}/schedule/add`, {
    date,
    name,
    contentType,
    startTime
  });
}*/


export const deleteEvent = async (id) => (await api.delete(`/schedule/${id}`)).data;


export const rescheduleEvent = async (id, newStartTime) =>
  (await api.put(`/schedule/${id}/reschedule`, { newStartTime })).data;


export const addHost = async (id, hostName) =>
  (await api.post(`/schedule/${id}/host`, { name: hostName })).data;


export const addGuest = async (id, guestName) =>
  (await api.post(`/schedule/${id}/guest`, { name: guestName })).data;

// Login contributor
export const login = async (email, password) =>
  api.post("/login?useCookies=true", { email, password });

export const logout = async () => {
  return api.post("/logout");
};
// Create contributor profile (after login)
export const createContributor = async (data) => {
  return (await api.post("/contributors/create", data)).data;
};

export const getMyContributor = async () =>
  (await api.get("/contributors/me")).data;

// Calculate salary
export const calculateContributor = async (id) => {
  return (await api.get(`/contributors/${id}/calculate`)).data;
};