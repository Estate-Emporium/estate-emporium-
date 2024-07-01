import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Authenticator } from "@aws-amplify/ui-react";
import "./App.css";
import "@aws-amplify/ui-react/styles.css";
import "./App.css";

function App() {
  return (
    <Authenticator socialProviders={["google"]} className="page-container">
      <BrowserRouter>
        <main>
          <Routes>
            <Route path="/" element={<div>Home</div>} />
          </Routes>
        </main>
      </BrowserRouter>
    </Authenticator>
  );
}

export default App;
