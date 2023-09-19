import React from "react";
import { useSelector } from "react-redux";
import { useParams } from "react-router-dom";

import LoadingButton from "@mui/lab/LoadingButton";
import { Typography } from "@mui/material";
import Box from "@mui/material/Box";

import ErrorPage from "../../pages/error";
import { PRODUCTS_ROUTE } from "../../routes";
import {
  useDeleteProductMutation,
  useGetProductByIdQuery,
  useUpdateProductMutation,
} from "../../services/productService";
import { useToastMessage } from "../../shared/hooks/useToastMessage";
import { RootState } from "../../store";
import { Role } from "../user/types";

import ProductForm from "./form";
import { ProductFormFields } from "./types";

const Product = () => {
  const { id } = useParams();

  const user = useSelector((state: RootState) => state.user);
  const canUserEdit = user?.roles === Role.salesman;

  const { data, error, isLoading } = useGetProductByIdQuery(id);

  const [
    updateProduct,
    { isLoading: isSubmitting, error: updateError, isSuccess },
  ] = useUpdateProductMutation();

  const [
    deleteProduct,
    {
      isLoading: isDeleting,
      error: deleteError,
      isSuccess: isSuccessfullyDeleted,
    },
  ] = useDeleteProductMutation();

  useToastMessage({
    isSuccess,
    error: updateError,
    successMessage: "Product successfully updated",
    errorMessage: "There was an error when trying to update product",
    successNavigateRoute: PRODUCTS_ROUTE,
  });

  useToastMessage({
    isSuccess: isSuccessfullyDeleted,
    error: deleteError,
    successMessage: "Product successfully deleted",
    errorMessage: "There was an error when trying to delete product",
    successNavigateRoute: PRODUCTS_ROUTE,
  });

  const onSubmit = (product: ProductFormFields) =>
    updateProduct({ id, product });

  const DeleteButton = (
    <Box sx={{ position: "absolute", bottom: 20, right: 20 }}>
      <LoadingButton
        variant={"contained"}
        color={"error"}
        loading={isDeleting}
        onClick={() => deleteProduct(id)}
      >
        Delete Product
      </LoadingButton>
    </Box>
  );

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <ErrorPage error={!!error} isLoading={isLoading}>
        <Box
          sx={{
            display: "flex",
            justifyContent: "center",
            alignItems: "center",
            flexDirection: "row",
            gap: 3,
          }}
        >
          <Typography variant="h6">Product</Typography>
          {canUserEdit && DeleteButton}
        </Box>

        <ProductForm
          onSubmit={onSubmit}
          submitButtonLabel={"Edit"}
          product={data}
          isSubmitting={isSubmitting}
        />
      </ErrorPage>
    </Box>
  );
};

export default Product;
