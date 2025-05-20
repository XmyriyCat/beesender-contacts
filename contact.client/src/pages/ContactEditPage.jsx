import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import { useSnackbar } from "notistack";
import {
    createContact,
    fetchContactById,
    updateContact,
} from "../services/contactService";

const ContactFormPage = () => {
    const navigate = useNavigate();
    const { id } = useParams();
    const isEditMode = Boolean(id);
    const { enqueueSnackbar } = useSnackbar();

    const [formData, setFormData] = useState({
        name: "",
        phoneNumber: "",
        jobTitle: "",
        birthDate: "",
    });

    const [errors, setErrors] = useState({});

    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    useEffect(() => {
        if (isEditMode) {
            const loadContact = async () => {
                try {
                    setLoading(true);
                    const data = await fetchContactById(id);
                    // Format birthDate to yyyy-MM-dd for input[type=date]
                    const formattedData = {
                        ...data,
                        birthDate: data.birthDate ? data.birthDate.slice(0, 10) : "",
                    };
                    setFormData(formattedData);
                } catch (err) {
                    setError("Failed to load contact.");
                } finally {
                    setLoading(false);
                }
            };
            loadContact();
        }
    }, [id, isEditMode]);

    const validate = () => {
        const errs = {};
        // Name validation
        if (!formData.name.trim()) {
            errs.name = "Name is required.";
        } else if (formData.name.length > 100) {
            errs.name = "Name cannot exceed 100 characters.";
        }

        // PhoneNumber validation
        if (!formData.phoneNumber.trim()) {
            errs.phoneNumber = "Phone number is required.";
        } else if (formData.phoneNumber.length < 10) {
            errs.phoneNumber = "Phone number must be at least 10 characters.";
        } else if (formData.phoneNumber.length > 20) {
            errs.phoneNumber = "Phone number cannot exceed 20 characters.";
        }

        // JobTitle validation
        if (!formData.jobTitle.trim()) {
            errs.jobTitle = "Job title is required.";
        } else if (formData.jobTitle.length > 50) {
            errs.jobTitle = "Job title cannot exceed 50 characters.";
        }

        // BirthDate validation
        if (!formData.birthDate) {
            errs.birthDate = "Birth date is required.";
        } else {
            const birthDateObj = new Date(formData.birthDate);
            const today = new Date();
            if (isNaN(birthDateObj.getTime())) {
                errs.birthDate = "Birth date must be a valid date.";
            } else if (birthDateObj > today) {
                errs.birthDate = "Birth date cannot be in the future.";
            } else {
                // Check age <= 120 years
                const age = today.getFullYear() - birthDateObj.getFullYear();
                if (age > 120) {
                    errs.birthDate = "Age cannot be more than 120 years.";
                }
            }
        }

        return errs;
    };

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
        // Clear error for this field on change
        setErrors((prev) => ({ ...prev, [e.target.name]: undefined }));
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const validationErrors = validate();
        setErrors(validationErrors);
        if (Object.keys(validationErrors).length > 0) {
            // Validation failed; do not submit
            return;
        }

        try {
            if (isEditMode) {
                await updateContact(id, formData);
                enqueueSnackbar("Contact updated successfully!", { variant: "success" });
            } else {
                await createContact(formData);
                enqueueSnackbar("Contact created successfully!", { variant: "success" });
            }
            navigate("/contacts");
        } catch (err) {
            enqueueSnackbar("Failed to submit contact.", { variant: "error" });
        }
    };

    return (
        <div className="container mt-4">
            <h2 className="text-secondary mb-4">{isEditMode ? "Edit Contact" : "Create Contact"}</h2>

            {error && <p className="text-danger">{error}</p>}

            <form onSubmit={handleSubmit} noValidate>
                <div className="mb-3">
                    <label className="form-label">Name</label>
                    <input
                        type="text"
                        className={`form-control ${errors.name ? "is-invalid" : ""}`}
                        name="name"
                        value={formData.name}
                        onChange={handleChange}
                    />
                    {errors.name && <div className="invalid-feedback">{errors.name}</div>}
                </div>

                <div className="mb-3">
                    <label className="form-label">Phone Number</label>
                    <input
                        type="text"
                        className={`form-control ${errors.phoneNumber ? "is-invalid" : ""}`}
                        name="phoneNumber"
                        value={formData.phoneNumber}
                        onChange={handleChange}
                    />
                    {errors.phoneNumber && <div className="invalid-feedback">{errors.phoneNumber}</div>}
                </div>

                <div className="mb-3">
                    <label className="form-label">Job Title</label>
                    <input
                        type="text"
                        className={`form-control ${errors.jobTitle ? "is-invalid" : ""}`}
                        name="jobTitle"
                        value={formData.jobTitle}
                        onChange={handleChange}
                    />
                    {errors.jobTitle && <div className="invalid-feedback">{errors.jobTitle}</div>}
                </div>

                <div className="mb-3">
                    <label className="form-label">Birth Date</label>
                    <input
                        type="date"
                        className={`form-control ${errors.birthDate ? "is-invalid" : ""}`}
                        name="birthDate"
                        value={formData.birthDate}
                        onChange={handleChange}
                    />
                    {errors.birthDate && <div className="invalid-feedback">{errors.birthDate}</div>}
                </div>

                <button type="submit" className="btn btn-success me-2">
                    {isEditMode ? "Update Contact" : "Create Contact"}
                </button>
                <button
                    type="button"
                    className="btn btn-primary"
                    onClick={() => navigate("/contacts")}
                >
                    Cancel
                </button>
            </form>
        </div>
    );
};

export default ContactFormPage;