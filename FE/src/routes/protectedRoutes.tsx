import { RouteObject } from "react-router-dom";

import Dashboard from "../components/dashboard";
import OrderList from "../components/order/orderList";
import Product from "../components/product";
import Cart from "../components/product/cart";
import NewProduct from "../components/product/NewProduct";
import ProductList from "../components/product/productList";
import UserProfile from "../components/user/profile/UserProfile";
import { Role, UserStatus } from "../components/user/types";
import UserList from "../components/user/userList";
import HomePage from "../pages/home/HomePage";
import NotFoundPage from "../pages/notFound/NotFoundPage";
import { UserState } from "../store/user/userSlice";

import {
  ALL_ORDERS_ROUTE,
  CART_ROUTE,
  DEALERS_ROUTE,
  HOME_ROUTE,
  MY_ORDERS_ROUTE,
  NEW_ORDERS_ROUTE,
  NEW_PRODUCT_ROUTE,
  PENDING_REQUESTS_ROUTE,
  PREVIOUS_ORDERS_ROUTE,
  PRODUCTS_ROUTE,
  PROFILE_ROUTE,
  ROOT_ROUTE,
} from "./index";

const protectedRoutes = (
  role: Role | null,
  user: UserState | null
): RouteObject[] => {
  const isUserAdmin = role === Role.admin;
  const isUserSalesman = role === Role.salesman;
  const isUserCustomer = role === Role.customer;

  const isUserLoggedIn = isUserAdmin || isUserSalesman || isUserCustomer;

  const canUserAddProduct =
    isUserSalesman && user?.status === UserStatus.APPROVED;

  return [
    {
      path: ROOT_ROUTE,
      element: <Dashboard />,
      children: [
        // HOME
        {
          path: ROOT_ROUTE,
          element: <HomePage />,
        },
        {
          path: HOME_ROUTE,
          element: <HomePage />,
        },
        // USER
        {
          ...(isUserLoggedIn && {
            path: PROFILE_ROUTE,
            element: <UserProfile />,
          }),
        },
        // PRODUCT
        {
          ...(canUserAddProduct && {
            path: NEW_PRODUCT_ROUTE,
            element: <NewProduct />,
          }),
        },
        {
          ...((isUserSalesman || isUserAdmin || isUserCustomer) && {
            path: PRODUCTS_ROUTE,
            element: <ProductList />,
          }),
        },
        {
          ...((isUserSalesman || isUserCustomer) && {
            path: `${PRODUCTS_ROUTE}/:id`,
            element: <Product />,
          }),
        },
        {
          ...(isUserCustomer && {
            path: CART_ROUTE,
            element: <Cart />,
          }),
        },
        // ORDER
        {
          ...(isUserCustomer && {
            path: PREVIOUS_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        {
          ...((isUserSalesman || isUserCustomer) && {
            path: NEW_ORDERS_ROUTE,
            element: <OrderList newOrders />,
          }),
        },
        {
          ...(isUserSalesman && {
            path: MY_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        {
          ...(isUserAdmin && {
            path: ALL_ORDERS_ROUTE,
            element: <OrderList />,
          }),
        },
        // DEALER
        {
          ...(isUserAdmin && {
            path: PENDING_REQUESTS_ROUTE,
            element: <UserList pendingRequests />,
          }),
        },
        {
          ...(isUserAdmin && {
            path: DEALERS_ROUTE,
            element: <UserList />,
          }),
        },
        // OTHER
        {
          path: "*",
          element: <NotFoundPage />,
        },
      ],
    },
  ];
};

export default protectedRoutes;
