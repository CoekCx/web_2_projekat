import { UserStatus } from "../../components/user/types";

export const getUserStatusColor = (userStatus: UserStatus) => {
  switch (userStatus) {
    case UserStatus.APPROVED:
      return "green";
    case UserStatus.DISAPPROVED:
      return "red";
    default:
      return "blue";
  }
};
