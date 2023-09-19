import {
  Role,
  User,
  UserCredentials,
  UserFormFields,
} from "../components/user/types";

import { api, userTagType } from "./service";

interface UserDto extends Omit<User, "roles"> {
  roles: { id: Role }[];
}

export const userApi = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getUser: builder.query<UserDto, void>({
      query: () => `/api/administration/users/me`,
      providesTags: [userTagType],
    }),
    // MUTATIONS
    register: builder.mutation<User, Omit<UserFormFields, "repeatedPassword">>({
      query: (user: Omit<UserFormFields, "repeatedPassword">) => ({
        url: `/api/administration/users`,
        method: "POST",
        body: user,
      }),
    }),
    login: builder.mutation<string, UserCredentials>({
      query: (userCredentials: UserCredentials) => ({
        url: `/api/administration/users/login`,
        method: "PUT",
        body: userCredentials,
        responseHandler: "text",
      }),
    }),
    loginWithGoogle: builder.mutation<string, string>({
      query: (token: string) => ({
        url: `/api/administration/users/signin-google`,
        method: "PUT",
        body: { role: Role.customer, token },
        responseHandler: "text",
      }),
    }),
    updateUser: builder.mutation<
      User,
      Omit<UserFormFields, "repeatedPassword">
    >({
      query: (user: Omit<UserFormFields, "repeatedPassword">) => ({
        url: `/api/administration/users`,
        method: "PUT",
        body: user,
      }),
      invalidatesTags: [userTagType],
    }),
    uploadProfileImage: builder.mutation<string, { imageId: string }>({
      query: (params) => ({
        url: `/api/administration/users/profile-image`,
        method: "PUT",
        body: params,
      }),
      invalidatesTags: [userTagType],
    }),
  }),
  overrideExisting: false,
});

export const {
  useGetUserQuery,
  useLazyGetUserQuery,
  useLoginMutation,
  useLoginWithGoogleMutation,
  useRegisterMutation,
  useUpdateUserMutation,
  useUploadProfileImageMutation,
} = userApi;
