import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Authenticator } from "@aws-amplify/ui-react";
import "./App.css";
import "@aws-amplify/ui-react/styles.css";
import LandingPage from "./pages/landing-page";
import SalesPage from "./pages/sales-list-page";

function App() {
  return (
    <Authenticator
      socialProviders={["google"]}
      hideSignUp
      className="page-container"
    >
      <BrowserRouter>
        <main>
          <Routes>
            <Route path="/" element={<LandingPage />} />
            <Route path="/sales-list" element={<SalesPage />} />
          </Routes>
        </main>
      </BrowserRouter>
    </Authenticator>
  );
}

export default App;
