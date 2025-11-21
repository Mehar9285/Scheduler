

import React from "react";
import { Card, Row, Col } from "react-bootstrap";
import { FaInstagram, FaFacebook } from "react-icons/fa";
import kitty from "../images/kitty.jpg";
import rebecca from "../images/rebecca.jpg";
import idaengberg from "../images/ida.jpg";
import clabbe from "../images/clabbe.jpg";
import avicii from "../images/avicii.jpg";
import alesso from "../images/alesso.png";

const DJs = [
 { id: "d1", name: "Kitty Jutbring", bio: "Christer i P3", img: kitty },
  { id: "d2", name: "Rebecca & Fiona", bio: "P3 Klubbmix med Rebecca & Fiona", img: rebecca },
  { id: "d3", name: "Ida Engberg", bio: "Awakenings Podcast #015", img: idaengberg },
  { id: "d4", name: "Clabbe", bio: "DJ Mix", img: clabbe },
  { id: "d5", name: "Avicii", bio: "Tronik Show", img: avicii },
  { id: "d6", name: "Alesso", bio: "Ghetto House Radio (GHR)", img: alesso },
];

export default function DJSection() {
  return (
    <section className="py-5">
      <div className="container">
        <h2 className="mb-4">Meet Our DJ's</h2>
        <Row>
          {DJs.map((dj) => (
            <Col key={dj.id} xs={12} md={6}>
              <Card className="dj-card mb-3">
                <Card.Body className="d-flex gap-3 align-items-center">
                  
                  <img src={dj.img} alt={dj.name} className="rounded-circle"
                   style={{ width: 96, height: 96, objectFit: "cover" }} />
                  <div>
                    <h5>{dj.name}</h5>
                    <p className="mb-1">{dj.bio}</p>
                    
                    <div className="d-flex gap-2">
                      <a href="#" aria-label="Instagram"><FaInstagram /></a>
                      <a href="#" aria-label="Facebook"><FaFacebook /></a>
                    </div>
                  </div>
                </Card.Body>
              </Card>
            </Col>
          ))}
        </Row>
      </div>
    </section>
  );
}