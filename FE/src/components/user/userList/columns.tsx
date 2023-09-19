import React, { useState } from "react";

import CheckCircleIcon from "@mui/icons-material/CheckCircle";
import UnpublishedIcon from "@mui/icons-material/Unpublished";
import { LoadingButton } from "@mui/lab";
import { Box, Tooltip, Typography } from "@mui/material";

import {
  useApproveRequestMutation,
  useDisapproveRequestMutation,
} from "../../../services/dealerSevice";
import { useToastMessage } from "../../../shared/hooks/useToastMessage";
import { User, UserStatus } from "../types";

export const useUserColumns = (pendingRequests?: boolean) => {
  const [approvingRequestId, setApprovingRequestId] = useState<string | null>();
  const [disapprovingRequestId, setDisapprovingRequestId] = useState<
    string | null
  >();

  const [
    approveRequest,
    {
      isLoading: isApproving,
      isSuccess: isApproveSuccess,
      error: isApproveError,
    },
  ] = useApproveRequestMutation();
  const [
    disaproveRequest,
    {
      isLoading: isDisapproving,
      isSuccess: isDisapproveSuccess,
      error: isDisapproveError,
    },
  ] = useDisapproveRequestMutation();

  // approve toasts
  useToastMessage({
    isSuccess: isApproveSuccess,
    error: isApproveError,
    successMessage: "Request successfully approved",
    errorMessage: "There was a problem when trying to approve a request",
  });

  // disapprove toasts
  useToastMessage({
    isSuccess: isDisapproveSuccess,
    error: isDisapproveError,
    successMessage: "Request successfully disapproved",
    errorMessage: "There was a problem when trying to disapprove a request",
  });

  const RowActions = (row: User) => (
    <Box
      sx={{
        display: "flex",
        alignItems: "center",
        flexDirection: "row",
      }}
    >
      {row.status === UserStatus.PENDING && (
        <Typography variant="body2" sx={{ mr: 3 }}>
          pending...
        </Typography>
      )}

      {(pendingRequests || row.status === UserStatus.PENDING) && (
        <>
          <Tooltip title="Approve">
            <LoadingButton
              sx={{ fontSize: 12 }}
              loading={isApproving && approvingRequestId === row.id}
              onClick={() => {
                setApprovingRequestId(row.id);
                approveRequest(row.id);
              }}
            >
              <CheckCircleIcon />
            </LoadingButton>
          </Tooltip>

          <Tooltip title="Disapprove">
            <LoadingButton
              sx={{ fontSize: 12 }}
              loading={isDisapproving && disapprovingRequestId === row.id}
              onClick={() => {
                setDisapprovingRequestId(row.id);
                disaproveRequest(row.id);
              }}
            >
              <UnpublishedIcon color={"error"} />
            </LoadingButton>
          </Tooltip>
        </>
      )}
    </Box>
  );

  const userColumns = [
    {
      id: "actions",
      label: "",
      format: (row: User) => RowActions(row),
    },
    {
      id: "firstName",
      label: "First Name",
    },
    {
      id: "lastName",
      label: "Last Name",
    },
    {
      id: "email",
      label: "Email",
    },
  ];

  return userColumns;
};
