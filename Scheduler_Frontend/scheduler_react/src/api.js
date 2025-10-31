import axios from "axios";

const API_URL = "https://localhost:7063";

export const api = axios.create({
  baseURL: API_URL,
  headers: {
    "Accept": "application/json",
    "Content-Type": "application/json"
  }
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

  
  return axios.post(`${API_URL}/schedule/add`, {
    date,
    name,
    contentType,
    startTime
  });
}


export const deleteEvent = async (id) => (await api.delete(`/schedule/${id}`)).data;


export const rescheduleEvent = async (id, newStartTime) =>
  (await api.put(`/schedule/${id}/reschedule`, { newStartTime })).data;


export const addHost = async (id, hostName) =>
  (await api.post(`/schedule/${id}/host`, { name: hostName })).data;


export const addGuest = async (id, guestName) =>
  (await api.post(`/schedule/${id}/guest`, { name: guestName })).data;