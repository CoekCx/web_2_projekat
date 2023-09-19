import React from "react";
import { Link } from "react-router-dom";

import { Box, Typography } from "@mui/material";

import { ROOT_ROUTE } from "../../routes";

export default function NotFoundPage() {
  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
        minHeight: "100vh",
      }}
    >
      <Typography variant="h1">404</Typography>
      <Typography variant="h6">
        The page you’re looking for doesn’t exist.
      </Typography>
      <Link to={ROOT_ROUTE}>Back Home</Link>
    </Box>
  );
}
