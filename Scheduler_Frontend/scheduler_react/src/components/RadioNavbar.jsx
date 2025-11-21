import React, { useState } from "react";
import { Navbar, Nav, Container, Form, Button, Dropdown } from "react-bootstrap";
import { Link } from "react-router-dom";
import { FaSearch, FaHeadphones, FaRegHeart } from "react-icons/fa";
import { useStore } from "../store/useStore";

export default function RadioNavbar() {
 
  const [search, setSearch] = useState("");
  
  const myList = useStore((s) => s.myList);

  const removeFromList = useStore((s) => s.removeFromList);

  const onSearch = (e) => {
    e.preventDefault();
    console.log("Searching for:", search);
  };

  return (
   <Navbar expand="lg" variant="dark" className="radio-navbar">
      <Container>
        <Navbar.Brand as={Link} to="/">
          <FaHeadphones className="me-2" /> RadioWave
        </Navbar.Brand>

        <Navbar.Toggle aria-controls="nav" />
        <Navbar.Collapse id="nav">
          {/* Nav links */}
          <Nav className="me-auto">
            <Nav.Link as={Link} to="/">Home</Nav.Link>
            <Nav.Link as={Link} to="/dj">DJs</Nav.Link>
            <Nav.Link as={Link} to="/schedule">Schedule</Nav.Link>
            <Nav.Link as={Link} to="/about">About</Nav.Link>
            <Nav.Link as={Link} to="/contact">Contact</Nav.Link>
          </Nav>

          <Form className="d-flex me-2" onSubmit={onSearch}>
            <Form.Control
              placeholder="Search shows..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              size="sm"
            />
            <Button variant="outline-light" type="submit" className="ms-2">
              <FaSearch />
            </Button>
          </Form>

          <Dropdown align="end">
            <Dropdown.Toggle variant="secondary" size="sm">
              <FaRegHeart className="me-1" /> My List ({myList.length})
            </Dropdown.Toggle>
            <Dropdown.Menu>

              {myList.length === 0 && <Dropdown.Item disabled>No items</Dropdown.Item>}
              
              {myList.map((s) => (
                <Dropdown.Item key={s.id} className="d-flex justify-content-between align-items-center">
                  <span>{s.name}</span>
                  <Button variant="link" size="sm" onClick={() => removeFromList(s.id)}>Remove</Button>
                </Dropdown.Item>
              ))}
            </Dropdown.Menu>
          </Dropdown>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}
