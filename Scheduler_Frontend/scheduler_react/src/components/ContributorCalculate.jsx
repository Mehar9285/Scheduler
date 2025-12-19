import { useEffect, useState } from "react";
import { getMyContributor, calculateContributor } from "../api";

export default function ContributorCalculate() {
  const [result, setResult] = useState(null);

  useEffect(() => {
  async function load() {
    try {
      const me = await getMyContributor();
      const calc = await calculateContributor(me.id);
      setResult(calc);
    } catch (err) {
      console.error("Not authenticated", err);
    }
  }
  load();
}, []);

  if (!result) return <p>Loading…</p>;

  return (
    <>
      <h4>Payment Calculation</h4>
      <p><b>Month:</b> {result.month}</p>
      <p><b>Hours:</b> {result.totalHours}</p>
      <p><b>Events:</b> {result.numberOfEvents}</p>
      <p><b>Total:</b> {result.totalAmount} SEK</p>
      <p><b>Total with VAT:</b> {result.totalAmountWithVAT} SEK</p>
    </>
  );
}
