import React, { useEffect, useState } from "react";
import { Link } from "react-router-dom";
import ContactsView from "../components/ContactsView";
import { fetchContacts, deleteContact } from "../services/contactService.js";
import { useSnackbar } from "notistack";

const Contacts = () => {
    const [contacts, setContacts] = useState([]);
    const [page, setPage] = useState(1);
    const [pageSize] = useState(6);
    const [totalItems, setTotalItems] = useState(1);
    const [hasNextPage, setHasNextPage] = useState(false);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState("");

    const [showModal, setShowModal] = useState(false);
    const [contactToDelete, setContactToDelete] = useState(null);

    const { enqueueSnackbar } = useSnackbar();

    const totalPages = Math.ceil(totalItems / pageSize);

    const loadContacts = async () => {
        try {
            setLoading(true);
            setError("");
            const data = await fetchContacts({ page, pageSize });
            setContacts(data.items);
            setTotalItems(data.totalItems);
            setHasNextPage(data.hasNextPage);
        } catch (err) {
            setError("Failed to load contacts.");
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        loadContacts();
    }, [page]);

    const handleDeleteClick = (contact) => {
        setContactToDelete(contact);
        setShowModal(true);
    };

    const confirmDelete = async () => {
        try {
            await deleteContact(contactToDelete.id);
            enqueueSnackbar(`Deleted ${contactToDelete.name}`, { variant: "success" });
            setShowModal(false);
            setContactToDelete(null);
            loadContacts();
        } catch (err) {
            enqueueSnackbar("Failed to delete contact.", { variant: "error" });
        }
    };

    const handlePrevPage = () => {
        if (page > 1) setPage(page - 1);
    };

    const handleNextPage = () => {
        if (hasNextPage) setPage(page + 1);
    };

    return (
        <div className="container mt-4">
            <div className="d-flex justify-content-between align-items-center mb-4">
                <h2 className="mb-0 text-secondary">Contacts</h2>
                <Link to="/contacts/create" className="btn btn-success">
                    <i className="bi bi-plus-circle me-2"></i>
                    Create Contact
                </Link>
            </div>

            {loading && <p>Loading contacts...</p>}
            {error && <p className="text-danger">{error}</p>}

            {!loading && !error && (
                <ContactsView contacts={contacts} onDelete={handleDeleteClick} />
            )}

            <div className="d-flex justify-content-between align-items-center mt-4">
                <button
                    className="btn btn-outline-secondary"
                    onClick={handlePrevPage}
                    disabled={page === 1}
                >
                    Previous
                </button>
                <span className="text-muted">Page {page} of {totalPages}</span>
                <button
                    className="btn btn-outline-secondary"
                    onClick={handleNextPage}
                    disabled={!hasNextPage}
                >
                    Next
                </button>
            </div>

            {showModal && (
                <div
                    className="modal fade show d-block"
                    tabIndex="-1"
                    style={{ backgroundColor: "rgba(0, 0, 0, 0.6)", backdropFilter: "blur(3px)" }}
                >
                    <div className="modal-dialog modal-dialog-centered">
                        <div className="modal-content shadow-lg rounded-4 border-0">
                            <div className="modal-header bg-danger text-white rounded-top-4">
                                <h5 className="modal-title">
                                    <i className="bi bi-exclamation-triangle-fill me-2"></i>
                                    Confirm Deletion
                                </h5>
                                <button
                                    type="button"
                                    className="btn-close btn-close-white"
                                    onClick={() => setShowModal(false)}
                                ></button>
                            </div>
                            <div className="modal-body text-center">
                                <p className="fs-5">
                                    Are you sure you want to delete
                                    <br />
                                    <strong className="text-danger">{contactToDelete?.name}</strong>?
                                </p>
                            </div>
                            <div className="modal-footer justify-content-center border-0 pb-4">
                                <button
                                    className="btn btn-outline-secondary px-4"
                                    onClick={() => setShowModal(false)}
                                >
                                    Cancel
                                </button>
                                <button className="btn btn-danger px-4" onClick={confirmDelete}>
                                    Yes, Delete
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default Contacts;
