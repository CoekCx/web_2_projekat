import { ReactNode } from "react";

import AddToPhotosIcon from "@mui/icons-material/AddToPhotos";
import DevicesIcon from "@mui/icons-material/Devices";
import GroupsIcon from "@mui/icons-material/Groups";
import HomeIcon from "@mui/icons-material/Home";
import InventoryIcon from "@mui/icons-material/Inventory";
import LocalShippingIcon from "@mui/icons-material/LocalShipping";
import PendingActionsIcon from "@mui/icons-material/PendingActions";
import TocIcon from "@mui/icons-material/Toc";

import {
  ALL_ORDERS_ROUTE,
  DEALERS_ROUTE,
  HOME_ROUTE,
  LOGIN_ROUTE,
  MY_ORDERS_ROUTE,
  NEW_ORDERS_ROUTE,
  PENDING_REQUESTS_ROUTE,
  PREVIOUS_ORDERS_ROUTE,
  PRODUCTS_ROUTE,
  REGISTER_ROUTE,
} from "../../routes";
import { Role } from "../user/types";

export interface NavItem {
  text: string;
  path: string;
  icon?: ReactNode;
}

const navItemsAmin = [
  { text: "Home", path: HOME_ROUTE, icon: <HomeIcon /> },
  {
    text: "Verification Requests",
    path: PENDING_REQUESTS_ROUTE,
    icon: <PendingActionsIcon />,
  },
  { text: "Dealers", path: DEALERS_ROUTE, icon: <GroupsIcon /> },
  { text: "Orders", path: ALL_ORDERS_ROUTE, icon: <TocIcon /> },
];

const navItemsSales = [
  { text: "Home", path: HOME_ROUTE, icon: <HomeIcon /> },
  { text: "Products", path: PRODUCTS_ROUTE, icon: <DevicesIcon /> },
  { text: "New Orders", path: NEW_ORDERS_ROUTE, icon: <AddToPhotosIcon /> },
  { text: "My Orders", path: MY_ORDERS_ROUTE, icon: <InventoryIcon /> },
];

const navItemsCustomer = [
  { text: "Home", path: HOME_ROUTE, icon: <HomeIcon /> },
  { text: "Products", path: PRODUCTS_ROUTE, icon: <DevicesIcon /> },
  {
    text: "On the way",
    path: NEW_ORDERS_ROUTE,
    icon: <LocalShippingIcon />,
  },
  {
    text: "Orders",
    path: PREVIOUS_ORDERS_ROUTE,
    icon: <InventoryIcon />,
  },
];

export const navItemsProtected = (role: Role | null) => {
  const isUserAdmin = role === Role.admin;
  const isUserSalesman = role === Role.salesman;
  const isUserCustomer = role === Role.customer;

  if (isUserAdmin) return navItemsAmin;
  else if (isUserSalesman) return navItemsSales;
  else if (isUserCustomer) return navItemsCustomer;
  return [];
};

export const navItemsUnprotected = [
  { text: "Login", path: LOGIN_ROUTE },
  { text: "Register", path: REGISTER_ROUTE },
];
