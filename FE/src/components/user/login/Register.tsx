import React, { useEffect } from "react";
import { useDispatch } from "react-redux";
import { NavLink, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { Paper, Typography } from "@mui/material";
import Box from "@mui/material/Box";
import { CredentialResponse, GoogleLogin } from "@react-oauth/google";

import { HOME_ROUTE, LOGIN_ROUTE } from "../../../routes";
import { api, dealerTagType } from "../../../services/service";
import {
  useLazyGetUserQuery,
  useLoginWithGoogleMutation,
  useRegisterMutation,
} from "../../../services/userService";
import UserForm from "../form";
import { UserFormFields } from "../types";

export default function Register() {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const [getUser] = useLazyGetUserQuery();
  const [register, { isLoading, error, isSuccess }] = useRegisterMutation();

  const [
    loginWithGoogle,
    {
      isLoading: isSubmitting,
      error: signInWithGoogleError,
      isSuccess: googleSignInSuccess,
      data: tokenWithGoogleLogin,
    },
  ] = useLoginWithGoogleMutation();

  const onSubmit = (data: UserFormFields) => {
    const { repeatedPassword, dateOfBirth, ...dataToSend } = data;
    register({
      ...dataToSend,
      dateOfBirth: new Date(dateOfBirth),
    });
    dispatch(api.util.invalidateTags([dealerTagType]));
  };

  const handleSignInWithGoogle = ({ credential }: CredentialResponse) => {
    loginWithGoogle(credential!);
  };

  useEffect(() => {
    const registerError = error || signInWithGoogleError;
    if (registerError) {
      toast.error("There was an error when trying to register");
    }
  }, [error, signInWithGoogleError]);

  useEffect(() => {
    const registerSuccess = isSuccess || googleSignInSuccess;
    if (registerSuccess) {
      if (tokenWithGoogleLogin) {
        localStorage.setItem("token", tokenWithGoogleLogin);
        getUser();
        navigate(HOME_ROUTE);
      } else {
        navigate(LOGIN_ROUTE);
      }
      toast.success("User registered successfully");
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSuccess, googleSignInSuccess, tokenWithGoogleLogin]);

  return (
    <Paper
      sx={{
        height: "100vh",
        width: "100vw",
        position: "relative",
        left: 0,
        top: 0,
        backgroundImage:
          "url(https://static.independent.co.uk/s3fs-public/thumbnails/image/2017/12/19/15/istock-463173435.jpg)",
        backgroundPosition: "center",
        backgroundSize: "cover",
        backgroundRepeat: "no-repeat",
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
        <Typography variant="h4">
          Visit Our <NavLink to={HOME_ROUTE}>Home Page</NavLink>
        </Typography>

        <Box sx={{ m: 3 }}>
          <GoogleLogin
            onSuccess={handleSignInWithGoogle}
            onError={console.log}
          />
        </Box>

        <UserForm
          onSubmit={onSubmit}
          submitButtonLabel={"Register"}
          isSubmitting={isLoading || isSubmitting}
        />

        <Typography variant="h6" sx={{ mt: 3 }}>
          Already have an account? -{" "}
          <NavLink to={LOGIN_ROUTE} className={"login-button"}>
            Login
          </NavLink>
        </Typography>
      </Box>
    </Paper>
  );
}
