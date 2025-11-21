import React, { useEffect } from "react";
import { Button } from "react-bootstrap";
import { useStore } from "../store/useStore";
import { FcMusic } from "react-icons/fc";
import { FaPlay } from "react-icons/fa6";
import backgroundImage from "../images/radiostation1.jpg";

export default function NowPlaying() {
  
  const [playing, setPlaying] = React.useState(false);

 
  const nowPlaying = useStore((s) => s.nowPlaying);
  const setNowPlaying = useStore((s) => s.setNowPlaying);

  
  useEffect(() => {
    if (!nowPlaying) {
      setNowPlaying({ id: "s1", name: "DJ Mix", host: "DJ Clabbe", time: "08:00" });
    }},
     []);

  
  const handleToggle = () => {
    setPlaying((p) => !p);
    console.log(playing ? "Stopped listening" : "Started listening (demo)");
  };

  return (
    <div className="hero"
    style={{
        backgroundImage: `linear-gradient(0deg, rgba(53, 16, 16, 0.5),
         rgba(28, 21, 21, 0.25)), 
        url(${backgroundImage})`,
      }}>
      <div className="container text-white">
        <h3 className="mb-2">Now Playing</h3>
        
        <h1 className="display-6">{nowPlaying?.name}</h1>
        <p className="lead">Host: {nowPlaying?.host}</p>
       
        <Button className="btn-listen" onClick={handleToggle}>
          {playing ? (
  <span>
    <FcMusic style={{ marginRight: "8px" }} /> Live Streaming...
  </span>
) : (
  <span>
    <FaPlay style={{ marginRight: "8px" }} /> Listen Live
  </span>
)}
        </Button>
      </div>
    </div>
  );
}