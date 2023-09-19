import { useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { SerializedError } from "@reduxjs/toolkit";
import { FetchBaseQueryError } from "@reduxjs/toolkit/query";

interface Params {
  isSuccess: boolean;
  successMessage: string;
  error: FetchBaseQueryError | SerializedError | undefined;
  errorMessage: string;
  successNavigateRoute?: string;
}

export const useToastMessage = ({
  isSuccess,
  error,
  successMessage,
  errorMessage,
  successNavigateRoute,
}: Params) => {
  const navigate = useNavigate();

  useEffect(() => {
    if (error) {
      toast.error(errorMessage);
    }
  }, [error, errorMessage]);

  useEffect(() => {
    if (isSuccess) {
      if (successNavigateRoute) {
        navigate(successNavigateRoute);
      }
      toast.success(successMessage);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSuccess, successMessage, successNavigateRoute]);
};
