import { FC } from "react";

import ErrorPage from "../../../pages/error";
import { useGetDealersQuery } from "../../../services/dealerSevice";
import Table from "../../../shared/table";
import { useTablePagination } from "../../../shared/table/hooks";

import { useUserColumns } from "./columns";

interface UserListProps {
  pendingRequests?: boolean;
}

const UserList: FC<UserListProps> = ({ pendingRequests }) => {
  const userColumns = useUserColumns(pendingRequests);

  const { page, itemsPerPage, handleChangePage, handleChangeItemsPerPage } =
    useTablePagination();

  const { data, error, isLoading } = useGetDealersQuery({
    page: page,
    itemsPerPage,
    pending: pendingRequests,
  });

  const { data: requests, totalCount } = data || {};

  const tableProps = {
    title: pendingRequests ? "Verification Requests" : "Dealers",
    page,
    itemsPerPage,
    handleChangePage,
    handleChangeItemsPerPage,
    totalCount,
  };

  return (
    <ErrorPage error={!!error} isLoading={isLoading}>
      <Table columns={userColumns} data={requests} {...tableProps} />
    </ErrorPage>
  );
};

export default UserList;
