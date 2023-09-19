import React from "react";

import Box from "@mui/material/Box";
import Paper from "@mui/material/Paper";

import ErrorPage from "../../../pages/error";
import {
  useGetUserQuery,
  useUpdateUserMutation,
} from "../../../services/userService";
import UserForm from "../form";
import { password } from "../form/fields";
import { Role, UserFormFields } from "../types";

const UserProfile = () => {
  const { data, error, isLoading } = useGetUserQuery();

  const [updateProfile, { isLoading: isSubmitting }] = useUpdateUserMutation();

  const onSubmit = (data: Omit<UserFormFields, "repeatedPassword">) => {
    const dataToSend = { ...data };

    if (dataToSend.password === "") {
      delete dataToSend[password];
    }
    updateProfile(dataToSend);
  };

  return (
    <Paper sx={{ p: 2 }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          flexDirection: "column",
        }}
      >
        <ErrorPage error={!!error} isLoading={isLoading}>
          <UserForm
            formTitle={"Profile"}
            onSubmit={onSubmit}
            submitButtonLabel={"Edit"}
            user={data && { ...data, roles: data.roles[0].id as Role }}
            isSubmitting={isSubmitting}
          />
        </ErrorPage>
      </Box>
    </Paper>
  );
};

export default UserProfile;
