

import React from "react";
import { useForm } from "react-hook-form";
import { Form, Button } from "react-bootstrap";

export default function ContactForm() {

  const { register, handleSubmit, reset, formState: { errors } } = useForm();

  
  const onSubmit = (data) => {
    console.log("Form data:", data); 
    alert("Thank you!");
    reset(); 
    
  };

  return (
    
    <Form onSubmit={handleSubmit(onSubmit)} className="mx-auto" style={{ maxWidth: 640 }}>
      <Form.Group className="mb-3">
        <Form.Label>Name</Form.Label>
        
        <Form.Control {...register("name", { required: true })} />
        {errors.name && <small className="text-danger">Name is required</small>}
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Email</Form.Label>
        <Form.Control type="email" {...register("email", { required: true })} />
        {errors.email && <small className="text-danger">Email is required</small>}
      </Form.Group>

      <Form.Group className="mb-3">
        <Form.Label>Message</Form.Label>
        <Form.Control as="textarea" rows={4} {...register("message")} />
      </Form.Group>

      <Button type="submit" className="btn-listen">Send</Button>
    </Form>
  );
}