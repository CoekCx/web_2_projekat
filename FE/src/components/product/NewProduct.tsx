import { Box } from "@mui/material";

import { PRODUCTS_ROUTE } from "../../routes";
import { useAddProductMutation } from "../../services/productService";
import { useToastMessage } from "../../shared/hooks/useToastMessage";

import ProductForm from "./form";
import { ProductFormFields } from "./types";

const NewProduct = () => {
  const [addItem, { isLoading: isSubmitting, error, isSuccess }] =
    useAddProductMutation();

  useToastMessage({
    isSuccess,
    error,
    successMessage: "Product added successfully",
    errorMessage: "There was an error when trying to add new product",
    successNavigateRoute: PRODUCTS_ROUTE,
  });

  const onSubmit = (data: ProductFormFields) => addItem(data);

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
      }}
    >
      <ProductForm
        onSubmit={onSubmit}
        submitButtonLabel={"Add"}
        formTitle={"New Product"}
        isSubmitting={isSubmitting}
      />
    </Box>
  );
};

export default NewProduct;
