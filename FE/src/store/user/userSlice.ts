import { createSlice } from "@reduxjs/toolkit";

import { Role, UserStatus } from "../../components/user/types";
import { userApi } from "../../services/userService";

export type UserState = {
  token: string | null;
  id: string | null;
  email: string | null;
  username: string | null;
  firstName: string | null;
  lastName: string | null;
  dob: Date | null;
  address: string | null;
  roles: Role | null;
  image: string | null;
  isLoggedIn: boolean;
  status: UserStatus | null;
};

const initialState: UserState = {
  token: null,
  id: null,
  email: null,
  username: null,
  firstName: null,
  lastName: null,
  dob: null,
  address: null,
  roles: null,
  image: null,
  isLoggedIn: false,
  status: null,
};

export const userSlice = createSlice({
  name: "user",
  initialState,
  reducers: {
    logout: () => initialState,
  },
  extraReducers: (builder) => {
    builder.addMatcher(
      userApi.endpoints.getUser.matchFulfilled,
      (state, { payload }) => {
        const { id, email, firstName, lastName, roles, status } = payload;

        state.isLoggedIn = true;
        state.id = id;
        state.email = email;
        state.firstName = firstName;
        state.lastName = lastName;
        state.roles = roles[0].id as Role;
        state.status = status;
      }
    );
  },
});

export const { logout } = userSlice.actions;

export default userSlice.reducer;
