import './App.css';
import { BrowserRouter as Router, Routes, Route, Navigate } from "react-router-dom";
import Contacts from "./pages/Contacts";
import ContactEdit from "./pages/ContactEditPage";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-icons/font/bootstrap-icons.css';
import { SnackbarProvider } from "notistack";

function App() {
  return (
    <SnackbarProvider maxSnack={3}>
      <Router>
        <Routes>
          <Route path="/" element={<Navigate to="/contacts" replace />} />
          <Route path="/contacts" element={<Contacts />} />
          <Route path="/contacts/create" element={<ContactEdit />} />
          <Route path="/contacts/edit/:id" element={<ContactEdit />} />
        </Routes>
      </Router>
    </SnackbarProvider>
  );
}

export default App;
