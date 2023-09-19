import { useSelector } from "react-redux";
import { NavLink } from "react-router-dom";

import AddIcon from "@mui/icons-material/Add";
import { Fab } from "@mui/material";
import Box from "@mui/material/Box";

import ErrorPage from "../../../pages/error";
import { NEW_PRODUCT_ROUTE, PRODUCTS_ROUTE } from "../../../routes";
import { useGetProductsQuery } from "../../../services/productService";
import Table from "../../../shared/table";
import { useTablePagination } from "../../../shared/table/hooks";
import { RootState } from "../../../store";
import { Role, UserStatus } from "../../user/types";

import { productColumns } from "./columns";

const ProductList = () => {
  const user = useSelector((state: RootState) => state.user);

  const isSalesman = user?.roles === Role.salesman;
  const canUserAddProduct = isSalesman && user?.status === UserStatus.APPROVED;

  const { page, itemsPerPage, handleChangePage, handleChangeItemsPerPage } =
    useTablePagination();

  const { data, error, isLoading } = useGetProductsQuery({
    page: page,
    itemsPerPage,
    onlyActive: true,
    isDealer: isSalesman,
  });

  const { data: products, totalCount } = data || {};

  const tableProps = {
    title: "Products",
    page,
    itemsPerPage,
    handleChangePage,
    handleChangeItemsPerPage,
    totalCount,
    navigateRoute: PRODUCTS_ROUTE,
  };

  return (
    <>
      {canUserAddProduct && (
        <Box sx={{ position: "fixed", top: 88, right: 40, zIndex: 500 }}>
          <NavLink to={NEW_PRODUCT_ROUTE}>
            <Fab color="primary" aria-label="add product" variant={"extended"}>
              <AddIcon />
            </Fab>
          </NavLink>
        </Box>
      )}

      <ErrorPage error={!!error} isLoading={isLoading}>
        <Table columns={productColumns} data={products} {...tableProps} />
      </ErrorPage>
    </>
  );
};

export default ProductList;
