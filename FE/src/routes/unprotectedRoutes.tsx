import { RouteObject } from "react-router-dom";

import Dashboard from "../components/dashboard";
import Login from "../components/user/login/Login";
import Register from "../components/user/login/Register";
import HomePage from "../pages/home/HomePage";

import { HOME_ROUTE, LOGIN_ROUTE, REGISTER_ROUTE, ROOT_ROUTE } from "./index";

const routes: RouteObject[] = [
  {
    path: ROOT_ROUTE,
    element: <Dashboard />,
    children: [
      {
        path: ROOT_ROUTE,
        element: <HomePage />,
      },
      {
        path: HOME_ROUTE,
        element: <HomePage />,
      },
    ],
  },
  {
    path: LOGIN_ROUTE,
    element: <Login />,
  },
  {
    path: REGISTER_ROUTE,
    element: <Register />,
  },
  {
    path: "*",
    element: <Login />,
  },
];

export default routes;
