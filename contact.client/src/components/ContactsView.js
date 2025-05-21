import React from "react";
import { Link } from "react-router-dom";

const ContactsView = ({ contacts, onDelete }) => {
    if (!Array.isArray(contacts)) {
        return <p className="text-danger">No contacts available or data is not an array.</p>;
    }

    return (
        <div className="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            {contacts.map((contact) => (
                <div key={contact.id} className="col">
                    <div className="card h-100 shadow-sm bg-light border border-secondary-subtle">
                        <div className="card-body d-flex flex-column">
                            <h5 className="card-title text-primary">{contact.name}</h5>
                            <p className="card-text">
                                <strong>Job Title:</strong> {contact.jobTitle || "No job title info."}
                            </p>
                            <p className="card-text">
                                <strong>Phone Number:</strong> {contact.phoneNumber || "No phone number info."}
                            </p>
                            <p className="card-text">
                                <strong>Birth Date:</strong> {contact.birthDate || "No birth date info."}
                            </p>

                            <div className="mt-auto d-flex justify-content-end gap-2">
                                <Link to={`/contacts/edit/${contact.id}`} className="btn btn-primary">
                                    <i className="bi bi-pencil-square me-1"></i>
                                    Edit
                                </Link>
                                <button
                                    className="btn btn-danger"
                                    onClick={() => onDelete(contact)}
                                >
                                    <i className="bi bi-trash"></i> Delete
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            ))}
        </div>
    );
};

export default ContactsView;