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
import AddEventForm from "./AddEventForm";
import ScheduleToday from "./ScheduleToday";
import ScheduleWeek from "./ScheduleWeek";

export default function SchedulePreview({ view }) {
  const [schedule, setSchedule] = useState([]);
  const [weekSchedule, setWeekSchedule] = useState([]);

  const loadToday = async () => setSchedule(await getTodaySchedule());
  const loadWeek = async () => setWeekSchedule(await getWeekSchedule());

  useEffect(() => {
    view === "today" ? loadToday() : loadWeek();
  }, [view]);

  const handleAddEvent = async (data) => {
    try {
      const startDateTime = new Date(`${data.date}T${data.startTime}:00`);
      await addEvent({ ...data, startTime: startDateTime.toISOString() });
      view === "today" ? loadToday() : loadWeek();
    } catch (err) {
      console.error(err);
      alert("Failed to add event.");
    }
  };

  const handleDelete = async (id) => {
    await deleteEvent(id);
    view === "today" ? loadToday() : loadWeek();
  };

  const handleReschedule = async (id) => {
    const newTime = prompt("Enter new start time (HH:mm):");
    if (!newTime) return;
    const date = new Date().toISOString().split("T")[0];
    const newStartTime = new Date(`${date}T${newTime}`).toISOString();
    await rescheduleEvent(id, newStartTime);
    view === "today" ? loadToday() : loadWeek();
  };

  const handleAddHost = async (id) => {
    const hostName = prompt("Enter host name:");
    if (!hostName) return;
    await addHost(id, hostName);
    view === "today" ? loadToday() : loadWeek();
  };

  const handleAddGuest = async (id) => {
    const guestName = prompt("Enter guest name:");
    if (!guestName) return;
    await addGuest(id, guestName);
    view === "today" ? loadToday() : loadWeek();
  };

  return (
    <div>
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