import ReactDOM from "react-dom/client";
import { ChakraProvider } from "@chakra-ui/react";
import App from "./App";
import "./index.css";
import { Amplify } from "aws-amplify";
import awsExports from "./aws-exports.ts";
import customTheme from "./theme";

Amplify.configure(awsExports);

ReactDOM.createRoot(document.getElementById("root")!).render(
  <ChakraProvider theme={customTheme}>
    <App />
  </ChakraProvider>
);
