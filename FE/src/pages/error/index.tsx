import React, { FC, ReactNode } from "react";

import { CircularProgress, Typography } from "@mui/material";
import Box from "@mui/material/Box";

import { drawerWidth } from "../../components/dashboard";

interface ErrorPageProps {
  error: boolean;
  children: ReactNode;
  isLoading?: boolean;
}
const ErrorPage: FC<ErrorPageProps> = ({ error, children, isLoading }) => {
  if (!error && !isLoading) return <>{children}</>;

  if (isLoading) {
    return (
      <Box
        sx={{
          position: "relative",
          width: { sm: `calc(100% - ${drawerWidth}px)` },
          height: "87vh",
          top: "50%",
          left: "50%",
          transform: "translate(-50%, -50%)",
        }}
      >
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            flexDirection: "column",
            position: "absolute",
            top: "50%",
            left: "50%",
            transform: "translate(-50%, -50%)",
            p: 4,
            backgroundColor: "white",
            opacity: 0.98,
            borderRadius: "4px",
          }}
        >
          <CircularProgress size={100} />
        </Box>
      </Box>
    );
  }

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <Typography variant="h3">Oops!</Typography>
      <Typography variant="h6">
        There was an error trying to load the data.
      </Typography>
    </Box>
  );
};

export default ErrorPage;
