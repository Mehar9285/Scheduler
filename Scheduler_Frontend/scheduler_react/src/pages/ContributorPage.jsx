import { useState } from "react";
import ContributorCreate from "../components/ContributorCreate";
import ContributorCalculate from "../components/ContributorCalculate";

export default function ContributorPage() {
  const [view, setView] = useState("create");

  return (
    <div className="container mt-4">
      <h2>Contributor Dashboard</h2>

      <div className="mb-3">
        <button
          className="btn btn-outline-primary me-2"
          onClick={() => setView("create")}
        >
          Create Contributor
        </button>

        <button
          className="btn btn-outline-success"
          onClick={() => setView("calculate")}
        >
          Calculate Payment
        </button>
      </div>

      {view === "create" && <ContributorCreate />}
      {view === "calculate" && <ContributorCalculate />}
    </div>
  );
}
