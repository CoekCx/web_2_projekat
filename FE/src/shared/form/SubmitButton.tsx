import React, { FC } from "react";

import LoadingButton from "@mui/lab/LoadingButton";

interface SubmitButtonProps {
  children: React.ReactNode;
  isLoading?: boolean;
}

const SubmitButton: FC<SubmitButtonProps> = ({ children, isLoading }) => {
  return (
    <LoadingButton
      type="submit"
      variant="contained"
      color="primary"
      loading={isLoading}
    >
      {children}
    </LoadingButton>
  );
};

export default SubmitButton;
