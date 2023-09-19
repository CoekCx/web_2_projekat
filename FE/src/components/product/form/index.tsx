import React, { FC, useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useSelector } from "react-redux";

import { yupResolver } from "@hookform/resolvers/yup";
import FolderIcon from "@mui/icons-material/Folder";
import {
  Avatar,
  CircularProgress,
  Fab,
  Stack,
  Typography,
} from "@mui/material";
import Box from "@mui/material/Box";

import { useAddImageMutation } from "../../../services/imageService";
import { useUploadProductImageMutation } from "../../../services/productService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { useImageUpload } from "../../../shared/hooks/useImageUpload";
import { RootState } from "../../../store";
import { Role } from "../../user/types";
import OrderProductModal from "../modal/OrderProductModal";
import { Product, ProductFormFields } from "../types";

import {
  amount,
  description,
  image,
  name,
  price,
  productFormValidation,
} from "./fields";

interface ProductFormProps {
  onSubmit: (data: ProductFormFields) => void;
  submitButtonLabel: string;
  formTitle?: string;
  product?: Product;
  isSubmitting?: boolean;
}

const ProductForm: FC<ProductFormProps> = ({
  submitButtonLabel,
  onSubmit,
  formTitle,
  isSubmitting,
  product,
}) => {
  const user = useSelector((state: RootState) => state.user);

  const canUserEdit = user?.roles === Role.salesman;

  const [addImage, { isLoading: isImageAdding }] = useAddImageMutation();

  const [uploadImage, { isLoading: isImageUploading }] =
    useUploadProductImageMutation();

  const { handleImageChange, imageSrc, setImageSrc } = useImageUpload({
    addImage,
    uploadImage,
    productId: product?.id,
  });

  const methods = useForm<ProductFormFields>({
    // @ts-ignore
    resolver: yupResolver(productFormValidation),
    defaultValues: product,
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  useEffect(() => {
    if (product?.productImage?.url) {
      setImageSrc(product?.productImage?.url);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [product?.productImage?.url]);

  const productImage = (
    <>
      {product && (
        <>
          <Avatar
            src={imageSrc}
            variant={"square"}
            alt="Product image"
            sx={{
              width: 500,
              height: 300,
              position: "relative",
              left: "50%",
              transform: "translate(-50%, 0)",
            }}
          >
            {isImageUploading || isImageAdding ? (
              <CircularProgress />
            ) : (
              <FolderIcon fontSize={"large"} />
            )}
          </Avatar>

          {canUserEdit && (
            <Fab
              color="primary"
              aria-label="add profile image"
              variant={"extended"}
              sx={{ p: 0 }}
            >
              <label className="file-upload">
                Upload Product Image
                <Input
                  field={image}
                  type={"file"}
                  accept={"image/*"}
                  onChange={handleImageChange}
                  sx={{
                    "& fieldset": { border: "none" },
                    margin: "auto",
                  }}
                />
              </label>
            </Fab>
          )}
        </>
      )}
    </>
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
      {formTitle && <Typography variant="h6">{formTitle}</Typography>}

      <Stack spacing={2}>
        <fieldset disabled={isSubmitting || !canUserEdit}>
          <FormProvider {...methods}>
            <Stack
              component="form"
              sx={{
                width: "50ch",
              }}
              spacing={2}
              noValidate
              autoComplete="off"
              onSubmit={handleSubmit(onSubmit)}
            >
              {productImage}
              <Input field={name} label={"Name"} />
              <Input field={price} label={"Price"} type="number" />
              <Input field={amount} label={"Amount"} type="number" />
              <Input
                field={description}
                label={"Product Description"}
                multiline
                maxRows={4}
              />

              {canUserEdit && (
                <SubmitButton isLoading={isSubmitting}>
                  {submitButtonLabel}
                </SubmitButton>
              )}
            </Stack>
          </FormProvider>
        </fieldset>

        <OrderProductModal product={product} />
      </Stack>
    </Box>
  );
};

export default ProductForm;
