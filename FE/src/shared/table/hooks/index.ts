import { useState } from "react";

export const useTablePagination = () => {
  const [page, setPage] = useState(1);
  const [itemsPerPage, setItemsPerPage] = useState(10);

  const handleChangePage = (event: unknown, newPage: number) => {
    setPage(newPage);
  };

  const handleChangeItemsPerPage = (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setItemsPerPage(+event.target.value);
    setPage(1);
  };

  return {
    page,
    itemsPerPage,
    handleChangePage,
    handleChangeItemsPerPage,
  };
};
