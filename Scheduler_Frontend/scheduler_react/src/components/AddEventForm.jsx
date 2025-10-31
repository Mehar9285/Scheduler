import React from "react";
import { useForm } from "react-hook-form";

function AddEventForm({ onAdd }) {
  const { register, handleSubmit, reset } = useForm();

  const onSubmit = (data) => {
    onAdd(data); 
    reset();
  };

  return (
    <form onSubmit={handleSubmit(onSubmit)} className="event-form">
      <h3>Add New Event</h3>

      <input {...register("name")} placeholder="Event title" required />
      <input {...register("date")} type="date" required />
      <input {...register("startTime")} type="time" required />

      <select {...register("contentType")} required>
        <option value="music">Music</option>
        <option value="studio">Studio</option>
        <option value="prerecording">PreRecording</option>
      </select>

      <button type="submit" className="btn-primary">Add Event</button>
    </form>
  );
}

export default AddEventForm;