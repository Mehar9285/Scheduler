
import React from "react";
import { Card, Button } from "react-bootstrap";
import { useStore } from "../store/useStore";

export default function ShowCard({ show }) {
  
  const addToList = useStore((s) => s.addToList);
  const setNowPlaying = useStore((s) => s.setNowPlaying);

  return (
    <Card className="h-100">
      <Card.Body>
        
        <Card.Title>{show.name}</Card.Title>
        <Card.Subtitle className="mb-2 text-muted">{show.time}</Card.Subtitle>
        <Card.Text>{show.description || "No description available."}</Card.Text>

        
        <div className="d-flex gap-2">
          
          <Button size="sm" onClick={() => setNowPlaying(show)}>Play</Button>

        
          <Button variant="outline-primary" size="sm" onClick={() => addToList(show)}>Add</Button>
        </div>
      </Card.Body>
    </Card>
  );
}