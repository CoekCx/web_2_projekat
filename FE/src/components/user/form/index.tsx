import React, { FC, useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import * as yup from "yup";

import { yupResolver } from "@hookform/resolvers/yup";
import AccountBoxIcon from "@mui/icons-material/AccountBox";
import ClearIcon from "@mui/icons-material/Clear";
import VerifiedIcon from "@mui/icons-material/Verified";
import {
  Avatar,
  Box,
  CircularProgress,
  Fab,
  Stack,
  Typography,
} from "@mui/material";
import Paper from "@mui/material/Paper";

import { useAddImageMutation } from "../../../services/imageService";
import { useUploadProfileImageMutation } from "../../../services/userService";
import DateInput from "../../../shared/form/DateInput";
import Input from "../../../shared/form/Input";
import SelectInput, { Option } from "../../../shared/form/SelectInput";
import SubmitButton from "../../../shared/form/SubmitButton";
import { useImageUpload } from "../../../shared/hooks/useImageUpload";
import { Role, User, UserFormFields, UserStatus } from "../types";

import {
  address,
  dob,
  email,
  image,
  lastName,
  name,
  password,
  repeatedPassword,
  roles,
  userFormValidation,
  username,
} from "./fields";

const roleOptions: Option[] = [
  { label: "Dealer", value: Role.salesman },
  { label: "Customer", value: Role.customer },
];

interface UserFormProps {
  onSubmit: (data: UserFormFields) => void;
  submitButtonLabel: string;
  formTitle?: string;
  user?: User;
  isSubmitting?: boolean;
}

const UserForm: FC<UserFormProps> = ({
  onSubmit,
  submitButtonLabel,
  formTitle,
  user,
  isSubmitting,
}) => {
  const [addImage, { isLoading: isImageAdding }] = useAddImageMutation();
  const [uploadImage, { isLoading: isImageUploading }] =
    useUploadProfileImageMutation();

  const { handleImageChange, imageSrc, setImageSrc } = useImageUpload({
    addImage,
    uploadImage,
  });

  const schema = yupResolver(
    yup
      .object({
        ...userFormValidation,
        // if user exists (not a register form) password should not be required
        [password]: !user ? yup.string().required() : yup.string(),
        [repeatedPassword]: !user
          ? yup
              .string()
              .oneOf([yup.ref(password)], "passwords do not match")
              .required()
          : yup.string().oneOf([yup.ref(password)], "passwords do not match"),
      })
      .required()
  );

  const methods = useForm<UserFormFields>({
    // @ts-ignore
    resolver: schema,
    defaultValues: {
      ...user,
      [roles]: user ? +user?.roles : Role.salesman,
    },
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  useEffect(() => {
    if (user?.profileImage?.url) {
      setImageSrc(user?.profileImage?.url);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [user?.profileImage?.url]);

  const isSalesman = user?.roles === Role.salesman;
  const canUserEdit =
    !isSalesman || (isSalesman && user?.status === UserStatus.APPROVED);

  const userImage = (
    <>
      {user && (
        <>
          <Paper sx={{ backgroundColor: "lightgray", p: 3 }}>
            <Avatar
              src={imageSrc}
              alt="User image"
              sx={{
                width: 150,
                height: 150,
                position: "relative",
                left: "50%",
                transform: "translate(-50%, 0)",
              }}
            >
              {isImageUploading || isImageAdding ? (
                <CircularProgress />
              ) : (
                <AccountBoxIcon fontSize={"large"} />
              )}
            </Avatar>
          </Paper>
          {canUserEdit && (
            <Fab
              color="primary"
              aria-label="add profile image"
              variant={"extended"}
              sx={{ p: 0 }}
            >
              <label className="file-upload">
                Upload Profile Image
                <Input
                  field={image}
                  type={"file"}
                  accept={"image/*"}
                  onChange={handleImageChange}
                  sx={{
                    "& fieldset": { border: "none" },
                    margin: "auto",
                  }}
                />
              </label>
            </Fab>
          )}
        </>
      )}
    </>
  );

  return (
    <>
      <Box sx={{ display: "flex", alignItems: "center" }}>
        {formTitle && <Typography variant="h6">{formTitle}</Typography>}
        {isSalesman && (
          <>
            {user?.status === UserStatus.APPROVED ? (
              <VerifiedIcon sx={{ ml: 1 }} />
            ) : (
              <ClearIcon sx={{ ml: 1 }} />
            )}
          </>
        )}
      </Box>

      <fieldset disabled={isSubmitting || !canUserEdit}>
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
            {userImage}
            <Input field={username} label={"Username"} />
            <Input field={email} label={"Email"} />
            <Stack
              direction="row"
              spacing={2}
              justifyContent="space-between"
              alignItems="flex-start"
            >
              <Input field={name} label={"First Name"} />
              <Input field={lastName} label={"Last Name"} />
            </Stack>
            <Stack
              direction="row"
              spacing={2}
              justifyContent="space-between"
              alignItems="flex-start"
            >
              <Input field={password} label={"Password"} type="password" />
              <Input
                field={repeatedPassword}
                label={"Repeated Password"}
                type="password"
              />
            </Stack>
            <Input field={address} label={"Address"} />
            <DateInput field={dob} label={"Date of Birth"} />
            <SelectInput
              field={roles}
              label={"User Role"}
              options={[
                ...roleOptions,
                ...(user?.roles === Role.admin
                  ? [
                      {
                        label: "Admin",
                        value: Role.admin,
                      },
                    ]
                  : []),
              ]}
              disabled={!!user}
            />

            {canUserEdit && (
              <SubmitButton isLoading={isSubmitting}>
                {submitButtonLabel}
              </SubmitButton>
            )}
          </Stack>
        </FormProvider>
      </fieldset>
    </>
  );
};

export default UserForm;
