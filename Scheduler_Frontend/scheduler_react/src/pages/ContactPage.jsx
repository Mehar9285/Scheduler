import React from "react";
import ContactForm from "../components/ContactForm";
import Footer from "../components/Footer";

export default function ContactPage() {
  return (
    <>
      <main className="container my-5">
        <h1>Contact</h1>
        <p>Send us a message or subscribe to our newsletter.</p>
        <ContactForm />
      </main>
      <Footer />
    </>
  );
}