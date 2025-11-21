import React, { useState } from "react";
import { Tabs, Tab } from "react-bootstrap";
import SchedulePreview from "../components/SchedulePreview";
import Footer from "../components/Footer";

export default function SchedulePage() {
  const [key, setKey] = useState("today");

  return (
    <div className="container my-5">
      <h1>Schedule</h1>
      <Tabs activeKey={key} onSelect={(k) => setKey(k)} className="mb-3">
        <Tab eventKey="today" title="Today">
          <SchedulePreview view="today" />
        </Tab>
        <Tab eventKey="week" title="Week">
          <SchedulePreview view="week" />
        </Tab>
      </Tabs>
      <Footer />
    </div>
    
  );
}