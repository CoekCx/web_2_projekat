import { Entity, Image } from "../../../shared/types";

export enum Role {
  salesman = 1,
  admin,
  customer,
}

export interface UserCredentials {
  email: string;
  password?: string;
}

export enum UserStatus {
  PENDING = 0,
  APPROVED,
  DISAPPROVED,
}

export interface User extends Entity, UserCredentials {
  username: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  address: string;
  roles: Role;
  profileImage: Image;
  status: UserStatus;
}

export interface UserFormFields extends Omit<User, "roles"> {
  repeatedPassword: string;
  roles: number;
}
