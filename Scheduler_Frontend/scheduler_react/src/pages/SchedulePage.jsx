import React, { useEffect, useState } from "react";
import {
  getTodaySchedule,
  getWeekSchedule,
  addEvent,
  deleteEvent,
  rescheduleEvent,
  addHost,
  addGuest,
} from "../api";
import AddEventForm from "../components/AddEventForm";
import ScheduleToday from "../components/ScheduleToday";
import ScheduleWeek from "../components/ScheduleWeek";

function SchedulePage({ view }) {
  const [schedule, setSchedule] = useState([]);
  const [weekSchedule, setWeekSchedule] = useState([]);

  const loadToday = async () => {
    const data = await getTodaySchedule();
    setSchedule(data);
  };

  const loadWeek = async () => {
    const data = await getWeekSchedule();
    setWeekSchedule(data);
  };

  useEffect(() => {
    if (view === "today") loadToday();
    else loadWeek();
  }, [view]);

  // Add Event
  const handleAddEvent = async (data) => {
  try {
    const startDateTime = new Date(`${data.date}T${data.startTime}:00`);
    const payload = {
      ...data,
      startTime: startDateTime.toISOString()
    };
    console.log("Adding event payload:", payload);

    await addEvent(payload);
    view === "today" ? loadToday() : loadWeek();
  } catch (err) {
    alert("Failed to add event. Check console.");
    console.error(err);
  }
};
  // Delete Event
  const handleDelete = async (id) => {
    await deleteEvent(id);
    view === "today" ? loadToday() : loadWeek();
  };

  // Reschedule Event
  const handleReschedule = async (id) => {
    const newTime = prompt("Enter new start time (HH:mm):");
    if (!newTime) return;
    const date = new Date().toISOString().split("T")[0]; 
    const newStartTime = new Date(`${date}T${newTime}`).toISOString();
    await rescheduleEvent(id, newStartTime);
    view === "today" ? loadToday() : loadWeek();
  };

  // Add Host
  const handleAddHost = async (id) => {
  const hostName = prompt("Enter host name:");
  if (!hostName) return;
  try {
    await addHost(id, hostName);
    setTimeout(() => {
      view === "today" ? loadToday() : loadWeek();
    }, 300);
  } catch (err) {
    console.error("Failed to add host:", err);
  }
};

  // Add Guest
  const handleAddGuest = async (id) => {
  const guestName = prompt("Enter guest name:");
  if (!guestName) return;
  try {
    await addGuest(id, guestName);
    setTimeout(() => {
      view === "today" ? loadToday() : loadWeek();
    }, 300);
  } catch (err) {
    console.error("Failed to add guest:", err);
  }
};

  return (
    <div className="page">
      <AddEventForm onAdd={handleAddEvent} />

      {view === "today" ? (
        <ScheduleToday
          schedule={schedule}
          onDelete={handleDelete}
          onReschedule={handleReschedule}
          onAddHost={handleAddHost}
          onAddGuest={handleAddGuest}
        />
      ) : (
        <ScheduleWeek
          weekSchedule={weekSchedule}
          onDelete={handleDelete}
          onReschedule={handleReschedule}
          onAddHost={handleAddHost}
          onAddGuest={handleAddGuest}
        />
      )}
    </div>
  );
}

export default SchedulePage;