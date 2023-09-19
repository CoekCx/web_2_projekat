import * as yup from "yup";

import { Role } from "../types";

enum RegisterFields {
  email = "email",
  password = "password",
  repeatedPassword = "repeatedPassword",
  username = "username",
  name = "firstName",
  lastName = "lastName",
  dob = "dateOfBirth",
  address = "address",
  roles = "roles",
  image = "profileImage",
}

export const {
  email,
  password,
  repeatedPassword,
  username,
  name,
  lastName,
  dob,
  address,
  roles,
  image,
} = RegisterFields;

export const userFormValidation = {
  [email]: yup.string().email().required(),
  [password]: yup.string().required(),
  [repeatedPassword]: yup
    .string()
    .oneOf([yup.ref(password)], "passwords do not match")
    .required(),
  [username]: yup.string().required(),
  [name]: yup.string().required(),
  [lastName]: yup.string().required(),
  [dob]: yup.date().required(),
  [address]: yup.string().required(),
  [roles]: yup
    .number()
    .oneOf(Object.keys(Role).map((key) => +key))
    .required(),
};
