import React from "react";
import ReactDOM from "react-dom/client";
import { Provider } from "react-redux";
import { BrowserRouter } from "react-router-dom";
import { ToastContainer } from "react-toastify";

import { createTheme, ThemeProvider } from "@mui/material";
import { lime, purple } from "@mui/material/colors";
import { GoogleOAuthProvider } from "@react-oauth/google";

import App from "./App";
import reportWebVitals from "./reportWebVitals";
import { store } from "./store";

import "react-toastify/dist/ReactToastify.css";
import "./index.css";

// fonts
import "./fonts/Montserrat/Montserrat-Black.ttf";

const theme = createTheme({
  palette: {
    primary: lime,
    secondary: purple,
  },
  typography: {
    allVariants: {
      fontFamily: "Montserrat",
      textTransform: "none",
    },
  },
});

const root = ReactDOM.createRoot(
  document.getElementById("root") as HTMLElement
);
root.render(
  <ThemeProvider theme={theme}>
    <GoogleOAuthProvider clientId={process.env.REACT_APP_CLIENT_ID!}>
      <React.StrictMode>
        <BrowserRouter>
          <Provider store={store}>
            <App />
            <ToastContainer
              theme="dark"
              position="bottom-left"
              autoClose={5000}
              hideProgressBar
            />
          </Provider>
        </BrowserRouter>
      </React.StrictMode>
    </GoogleOAuthProvider>
  </ThemeProvider>
);

reportWebVitals();
