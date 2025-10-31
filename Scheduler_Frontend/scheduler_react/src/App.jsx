import React, { useState } from "react";
import SchedulePage from "./pages/SchedulePage";
import "./App.css";
import radioLogo from "./images/radio.svg";
import { Tabs, Tab } from "react-bootstrap";

function App() {
  const [key, setKey] = useState("home");

  return (
    <div className="app-container">
      { }
      <div className="app-navbar">
        <img src={radioLogo} alt="Radio Logo" className="app-logo" />
        <h2 className="app-title">Radio Scheduler</h2>
      </div>

      { }
      <div className="app-tabs">
        <Tabs activeKey={key} onSelect={(k) => setKey(k)} className="tabs-nav">
          <Tab eventKey="home" title="Home">
            <h3>Welcome to Radio Scheduler</h3>
            <p className="text-muted">
              Manage daily and weekly schedules easily with Radio Scheduler
            </p>
          </Tab>
          <Tab eventKey="today" title="Today">
            <SchedulePage view="today" />
          </Tab>
          <Tab eventKey="week" title="Week">
            <SchedulePage view="week" />
          </Tab>
        </Tabs>
      </div>
    </div>
  );
}

export default App;