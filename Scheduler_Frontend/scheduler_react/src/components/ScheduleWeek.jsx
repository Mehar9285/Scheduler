import React from "react"; 
function ScheduleWeek({ weekSchedule, onDelete, onReschedule, onAddHost, onAddGuest }) 
{ 
    return ( <div className="schedule-container">
     <h2>Week Schedule</h2> 
     {weekSchedule.map((day, i) => ( <div key={i} className="day-block"> 
        <h3>{new Date(day.datee).toLocaleDateString()}</h3>
         {day.items.length === 0 ? ( <p>No events scheduled.</p> ):
          ( <ul className="schedule-list"> 
          {
          day.items.map((item) => ( <li key={item.id} className="schedule-item">
         <div className="schedule-details">
            <strong>{item.content?.title || item.title}
            </strong> 
                <br />
                 {new Date(item.startTime).toLocaleTimeString()}-{" "}
                     {new Date(item.endTime).toLocaleTimeString()} 
                        <br />
                        Type: {item.contentType || item.content?.type}
                        <br /> 
                       {item.contentType === "studio" && 
                       (
                       <>
                     {item.host && <span>Host: {item.host}</span>} 
                     <br />
                      {item.guests && item.guests.length > 0 && (
                      <span>Guests: {item.guests.join(", ")}</span>
                       )}
                     </>
                     )}
                     </div>
                      <div className="schedule-actions">
                    <button onClick={() => onDelete(item.id)}>Delete</button>
                 <button onClick={() => onReschedule(item.id)}>Reschedule</button>
                  {(item.contentType === "studio" || item.content?.$type?.includes("Studio")) && 
                ( <> <button onClick={() => onAddHost(item.id)}>Add Host</button> 
                <button onClick={() => onAddGuest(item.id)}>Add Guest</button> 
                </> 
                )} 
                </div>
              </li>
              ))} 
                </ul>
                )}
                 </div>
             ))} 
            </div>
          );
         } 
        export default ScheduleWeek;