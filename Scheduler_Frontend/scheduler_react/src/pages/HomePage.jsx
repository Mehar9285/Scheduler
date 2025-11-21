import React from "react";
import NowPlaying from "../components/NowPlaying";
import SchedulePreview from "../components/SchedulePreview";
import DJSection from "../components/DJSection";
import Footer from "../components/Footer";

export default function HomePage() {
  return (
    <>
      
      <NowPlaying />

      
      <main className="container my-5">
        <section className="mb-5">
          <DJSection />
        </section>

        <section className="mb-5">
            <h2>Upcoming Event's</h2>
          <SchedulePreview />
          
        </section>
      </main>

      
      <Footer />
    </>
  );
}