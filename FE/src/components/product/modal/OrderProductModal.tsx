import React, { FC, useState } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { toast } from "react-toastify";
import * as yup from "yup";

import { yupResolver } from "@hookform/resolvers/yup";
import {
  Box,
  Button,
  Dialog,
  DialogActions,
  DialogContent,
  DialogContentText,
  DialogTitle,
  Stack,
} from "@mui/material";

import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { RootState } from "../../../store";
import { addToCart } from "../../../store/cart/cartSlice";
import { quantity } from "../../order/form/fields";
import { Role } from "../../user/types";
import { Product } from "../types";

interface OrderProductModalProps {
  product: Product | undefined;
}

const OrderProductModal: FC<OrderProductModalProps> = ({ product }) => {
  const dispatch = useDispatch();
  const [open, setOpen] = useState(false);

  const user = useSelector((state: RootState) => state.user);
  const canUserOrder = user?.roles === Role.customer;

  const quantityValidation = yup
    .number()
    .transform((value) => (isNaN(value) ? undefined : value))
    .nullable()
    .min(1, "Quantity can not be less than 1")
    .max(product?.capacity ?? 0)
    .required();

  const schema = yup
    .object({
      [quantity]: quantityValidation,
    })
    .required();

  const methods = useForm<{ quantity: number }>({
    // @ts-ignore
    resolver: yupResolver(schema),
    mode: "onSubmit",
  });

  const { handleSubmit, getValues, reset } = methods;

  const handleClose = () => {
    setOpen(false);
  };

  const onSubmit = () => {
    const { quantity } = getValues();
    dispatch(addToCart({ ...product, quantity: +quantity }));
    toast.success("Product successfully added to cart");
    setOpen(false);
    reset();
  };

  return (
    <Stack>
      {canUserOrder && (
        <Button
          disabled={!((product?.capacity ?? 0) > 0)}
          variant={"contained"}
          onClick={() => setOpen(true)}
        >
          Add to Cart
        </Button>
      )}

      <Dialog open={open} onClose={handleClose}>
        <DialogTitle>Add to cart</DialogTitle>
        <DialogContent>
          <FormProvider {...methods}>
            <Stack
              component="form"
              spacing={2}
              noValidate
              autoComplete="off"
              onSubmit={handleSubmit(onSubmit)}
            >
              <DialogContentText>Add {product?.name} to Cart</DialogContentText>

              <Box
                sx={{
                  display: "flex",
                  alignItems: "flex-start",
                }}
              >
                <Box sx={{ width: "50%", mr: 2 }}>
                  <Input field={quantity} type="number" label="Quantity" />
                </Box>
                <SubmitButton>Add to cart</SubmitButton>
              </Box>
            </Stack>
          </FormProvider>
        </DialogContent>
        <DialogActions>
          <Button onClick={handleClose}>Cancel</Button>
        </DialogActions>
      </Dialog>
    </Stack>
  );
};

export default OrderProductModal;
