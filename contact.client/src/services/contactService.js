import axios from 'axios';
import ApiEndpoints from "../api/ApiEndpoints";

export const fetchContacts = async ({ page = 1, pageSize = 6 }) => {
    const response = await axios.get(ApiEndpoints.Contact.GetAll(page, pageSize));

    return response.data;
};

export const fetchContactById = async (id) => {
    const response = await axios.get(ApiEndpoints.Contact.Get(id));

    return response.data;
};

export const updateContact = async (id, data) => {
    const response = await axios.put(ApiEndpoints.Contact.Update(id), data);

    return response.data;
};

export const createContact = async (data) => {
    const response = await axios.post(ApiEndpoints.Contact.Create, data);

    return response.data;
};

export const deleteContact = async (id) => {
    const response = await axios.delete(ApiEndpoints.Contact.Delete(id));

    return response.data;
};
