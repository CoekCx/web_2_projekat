import React, { useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { NavLink, useNavigate } from "react-router-dom";
import { toast } from "react-toastify";
import * as yup from "yup";

import { yupResolver } from "@hookform/resolvers/yup";
import { Paper, Stack, Typography } from "@mui/material";
import Box from "@mui/material/Box";
import { CredentialResponse, GoogleLogin } from "@react-oauth/google";

import { HOME_ROUTE, REGISTER_ROUTE } from "../../../routes";
import {
  useLazyGetUserQuery,
  useLoginMutation,
  useLoginWithGoogleMutation,
} from "../../../services/userService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { UserCredentials } from "../types";

enum LoginFields {
  email = "email",
  password = "password",
}

const { email, password } = LoginFields;

const schema = yup
  .object({
    [email]: yup.string().email().required(),
    [password]: yup.string().required(),
  })
  .required();

export default function Login() {
  const navigate = useNavigate();

  const [getUser] = useLazyGetUserQuery();

  const [login, { isLoading, error, isSuccess, data: token }] =
    useLoginMutation();

  const [
    loginWithGoogle,
    {
      isLoading: isSubmitting,
      error: signInWithGoogleError,
      isSuccess: googleSignInSuccess,
      data: tokenWithGoogleLogin,
    },
  ] = useLoginWithGoogleMutation();

  const methods = useForm<UserCredentials>({
    // @ts-ignore
    resolver: yupResolver(schema),
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  const onSubmit = () => {
    login(methods.getValues());
  };

  const handleSignInWithGoogle = ({ credential }: CredentialResponse) => {
    loginWithGoogle(credential!);
  };

  useEffect(() => {
    if (error || signInWithGoogleError) {
      toast.error("There was an error when trying to login");
    }
  }, [error, signInWithGoogleError]);

  useEffect(() => {
    const loginSuccess = isSuccess || googleSignInSuccess;
    const tokenToSetInLocalStorage = token || tokenWithGoogleLogin;

    if (loginSuccess && tokenToSetInLocalStorage) {
      localStorage.setItem("token", tokenToSetInLocalStorage);
      // Result is handled in extraReducers in userSlice and will be added to the store automatically
      getUser();
      toast.success("User logged in successfully");
      navigate(HOME_ROUTE);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSuccess, googleSignInSuccess, token, tokenWithGoogleLogin]);

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

        <FormProvider {...methods}>
          <Stack
            component="form"
            sx={{
              width: "50ch",
            }}
            spacing={2}
            noValidate
            autoComplete="off"
            onSubmit={handleSubmit(onSubmit)}
          >
            <Input
              field={email}
              label={"Email"}
              sx={{ backgroundColor: "white" }}
            />
            <Input
              field={password}
              label={"Password"}
              type="password"
              sx={{ backgroundColor: "white" }}
            />

            <SubmitButton isLoading={isLoading || isSubmitting}>
              Login
            </SubmitButton>
          </Stack>
        </FormProvider>

        <Typography variant="h6" sx={{ mt: 3 }}>
          <NavLink to={REGISTER_ROUTE} className={"login-button"}>
            Register
          </NavLink>{" "}
          if you don't have an account
        </Typography>
      </Box>
    </Paper>
  );
}
