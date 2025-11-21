

import React from "react";
import { Container } from "react-bootstrap";
import { FaFacebook, FaInstagram,  FaRegCopyright } from "react-icons/fa";

export default function Footer() {
  return (
    <footer className="site-footer mt-5">
      <Container>
        <div> <FaRegCopyright/> 2025 RadioWave</div>
        <div className="mt-2">
          <a className="me-3" href="#" aria-label="Facebook"><FaFacebook /></a>
          <a className="me-3" href="#" aria-label="Instagram"><FaInstagram /></a>
        </div>
      </Container>
    </footer>
  );
}