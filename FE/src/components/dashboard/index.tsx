import React from "react";
import { useDispatch, useSelector } from "react-redux";
import { NavLink, Outlet } from "react-router-dom";

import LogoutIcon from "@mui/icons-material/Logout";
import MenuIcon from "@mui/icons-material/Menu";
import PersonIcon from "@mui/icons-material/Person";
import ShoppingCartIcon from "@mui/icons-material/ShoppingCart";
import {
  Badge,
  CssBaseline,
  Divider,
  Drawer,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import { googleLogout } from "@react-oauth/google";

import {
  CART_ROUTE,
  HOME_ROUTE,
  LOGIN_ROUTE,
  PROFILE_ROUTE,
} from "../../routes";
import { RootState } from "../../store";
import { logout } from "../../store/user/userSlice";
import { Role } from "../user/types";

import { NavItem, navItemsProtected, navItemsUnprotected } from "./navItems";

export const drawerWidth = 240;

export default function Dashboard(props: { window?: Window }) {
  const { window } = props;

  const dispatch = useDispatch();
  const { roles, isLoggedIn, firstName, lastName } = useSelector(
    (state: RootState) => state.user
  );

  const { products: productsInCart } = useSelector(
    (state: RootState) => state.cart
  );

  const numberOfProductsInCart = productsInCart?.length;

  const userMenu: NavItem[] = isLoggedIn
    ? navItemsProtected(roles)
    : navItemsUnprotected;

  const [mobileOpen, setMobileOpen] = React.useState(false);

  const handleDrawerToggle = () => {
    setMobileOpen(!mobileOpen);
  };

  const handleLogout = () => {
    googleLogout();
    localStorage.clear();
    dispatch(logout());
  };

  const drawer = (
    <div>
      <Toolbar>
        <Typography>
          {firstName} {lastName}
        </Typography>
      </Toolbar>
      <Divider />
      <List>
        {userMenu.map(({ text, icon, path }) => (
          <ListItem key={text} disablePadding>
            <ListItemButton component={NavLink} to={path}>
              {icon && <ListItemIcon>{icon}</ListItemIcon>}
              <ListItemText primary={text} />
            </ListItemButton>
          </ListItem>
        ))}
      </List>

      <Divider />

      {isLoggedIn && (
        <List>
          {roles === Role.customer && (
            <ListItem disablePadding>
              <ListItemButton component={NavLink} to={CART_ROUTE}>
                <ListItemIcon sx={{ textAlign: "center" }}>
                  <Badge badgeContent={numberOfProductsInCart} color="primary">
                    <ShoppingCartIcon />
                  </Badge>
                </ListItemIcon>
                <ListItemText primary={"My Cart"} />
              </ListItemButton>
            </ListItem>
          )}
          <ListItem disablePadding>
            <ListItemButton component={NavLink} to={PROFILE_ROUTE}>
              <ListItemIcon sx={{ textAlign: "center" }}>
                <PersonIcon />
              </ListItemIcon>
              <ListItemText primary={"Profile"} />
            </ListItemButton>
          </ListItem>
          <ListItem disablePadding>
            <ListItemButton
              onClick={handleLogout}
              component={NavLink}
              to={LOGIN_ROUTE}
            >
              <ListItemIcon sx={{ textAlign: "center" }}>
                <LogoutIcon />
              </ListItemIcon>
            </ListItemButton>
          </ListItem>
        </List>
      )}
    </div>
  );

  const container = window !== undefined ? () => document.body : undefined;

  return (
    <Box sx={{ display: "flex" }}>
      <CssBaseline />
      <AppBar
        position="fixed"
        sx={{
          width: { sm: `calc(100% - ${drawerWidth}px)` },
          ml: { sm: `${drawerWidth}px` },
        }}
      >
        <Toolbar>
          <IconButton
            color="inherit"
            aria-label="open drawer"
            edge="start"
            onClick={handleDrawerToggle}
            sx={{ mr: 2, display: { sm: "none" } }}
          >
            <MenuIcon />
          </IconButton>
          <NavLink to={HOME_ROUTE}>
            <Typography variant="h6" component="div">
              My Shop
            </Typography>
          </NavLink>
        </Toolbar>
      </AppBar>

      <Box
        component="nav"
        sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
        aria-label="mailbox folders"
      >
        {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
        <Drawer
          container={container}
          variant="temporary"
          open={mobileOpen}
          onClose={handleDrawerToggle}
          ModalProps={{
            keepMounted: true, // Better open performance on mobile.
          }}
          sx={{
            display: { xs: "block", sm: "none" },
            "& .MuiDrawer-paper": {
              boxSizing: "border-box",
              width: drawerWidth,
            },
          }}
        >
          {drawer}
        </Drawer>
        <Drawer
          variant="permanent"
          sx={{
            display: { xs: "none", sm: "block" },
            "& .MuiDrawer-paper": {
              boxSizing: "border-box",
              width: drawerWidth,
            },
          }}
          open
        >
          {drawer}
        </Drawer>
      </Box>
      <Box
        component="main"
        sx={{
          flexGrow: 1,
          p: 2,
          width: { sm: `calc(100% - ${drawerWidth}px)` },
        }}
      >
        <Toolbar />
        <Outlet />
      </Box>
    </Box>
  );
}
