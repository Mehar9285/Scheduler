import { useEffect, useState } from "react";
import { getMyContributor, createContributor } from "../api";

export default function ContributorPage() {
  const [contributor, setContributor] = useState(null);
  const [form, setForm] = useState({
    fullName: "",
    address: "",
    phone: "",
    email: "",
  });

  useEffect(() => {
    getMyContributor()
      .then(setContributor)
      .catch(() => setContributor(null));
  }, []);

  const handleCreate = async () => {
    const data = await createContributor(form);
    setContributor(data);
  };

  if (!contributor) {
    return (
      <div className="container mt-5">
        <h2>Create Contributor Profile</h2>

        <input
          className="form-control mb-2"
          placeholder="Full Name"
          onChange={(e) => setForm({ ...form, fullName: e.target.value })}
        />
        <input
          className="form-control mb-2"
          placeholder="Address"
          onChange={(e) => setForm({ ...form, address: e.target.value })}
        />
        <input
          className="form-control mb-2"
          placeholder="Phone"
          onChange={(e) => setForm({ ...form, phone: e.target.value })}
        />
        <input
          className="form-control mb-3"
          placeholder="Email"
          onChange={(e) => setForm({ ...form, email: e.target.value })}
        />

        <button className="btn btn-success" onClick={handleCreate}>
          Save Contributor
        </button>
      </div>
    );
  }

  return (
    <div className="container mt-5">
      <h2>Welcome {contributor.fullName}</h2>
      <p><b>Email:</b> {contributor.email}</p>
      <p><b>Phone:</b> {contributor.phone}</p>
    </div>
  );
}






/*import { useState } from "react";
import { createContributor } from "../api";

export default function ContributorCreate() {
  const [fullName, setFullName] = useState("");

  const submit = async () => {
    try {
      await createContributor({ fullName });
      alert("Contributor created");
    } catch {
      alert("Already created or error");
    }
  };

  return (
    <>
      <h4>Create Contributor</h4>
      <input
        className="form-control mb-2"
        placeholder="Full name"
        value={fullName}
        onChange={e => setFullName(e.target.value)}
      />
      <button className="btn btn-primary" onClick={submit}>
        Create
      </button>
    </>
  );
}*/
