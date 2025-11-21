import React from "react";
import Image from "../images/radiostation.jpg"; 

export default function AboutSection() {
  return (
    <section
      className="about-hero"
      style={{
        backgroundImage: `linear-gradient(rgba(75, 50, 50, 0.6), rgba(96, 68, 68, 0.6)),
         url(${Image})`,
      }}
    >
      <div className="about-content">
        <h1>About RadioWave</h1>
        <br/>
        <h2 className="lead">We bring music and community together.</h2>
         <br/>
        <p>
          Our mission is to deliver the best electronic, pop, and techno music,
          featuring talented DJs from Sweden and around the world.
        </p>
      </div>
    </section>
  );
}