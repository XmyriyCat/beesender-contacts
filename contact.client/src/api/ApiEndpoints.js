const API_BASE_URL = process.env.REACT_APP_API_BASE_URL; // Base api URL is here

export const ApiEndpoints = {
    Contact: {
        Create: `${API_BASE_URL}/contacts`,
        Get: (id) => `${API_BASE_URL}/contacts/${id}`,
        GetAll: (page, pagesize) => `${API_BASE_URL}/contacts?pagesize=${pagesize}&page=${page}`,
        Update: (id) => `${API_BASE_URL}/contacts/${id}`,
        Delete: (id) => `${API_BASE_URL}/contacts/${id}`,
    }
};

export default ApiEndpoints;