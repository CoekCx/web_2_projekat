import { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useRoutes } from "react-router-dom";

import { Box, CircularProgress } from "@mui/material";

import ErrorPage from "./pages/error";
import protectedRoutes from "./routes/protectedRoutes";
import unprotectedRoutes from "./routes/unprotectedRoutes";
import { useLazyGetUserQuery } from "./services/userService";
import { initializeCart } from "./store/cart/cartSlice";
import { RootState } from "./store";

import "./App.css";

function App() {
  const dispatch = useDispatch();

  const [getUser, { isError }] = useLazyGetUserQuery();

  const user = useSelector((state: RootState) => state.user);

  const isLoggedIn = user?.isLoggedIn;

  const token = localStorage.getItem("token");
  const cartState = localStorage.getItem("cart");

  const routes = useRoutes(
    isLoggedIn ? protectedRoutes(user?.roles, user) : unprotectedRoutes
  );

  useEffect(() => {
    if (token && !isLoggedIn) {
      getUser();
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  useEffect(() => {
    if (cartState) {
      try {
        const productsInCart = JSON.parse(cartState);
        dispatch(initializeCart(productsInCart));
      } catch (err) {
        console.log(err);
      }
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  if (token && !isLoggedIn) {
    return (
      <ErrorPage error={isError}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            flexDirection: "column",
            minHeight: "100vh",
          }}
        >
          <CircularProgress size={100} />
        </Box>
      </ErrorPage>
    );
  }

  return routes;
}

export default App;
