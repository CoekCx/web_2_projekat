import React, { useEffect } from "react";
import dayjs from "dayjs";
import { FormProvider, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { yupResolver } from "@hookform/resolvers/yup";
import { Box, List, Paper, Stack, Typography } from "@mui/material";

import { PRODUCTS_ROUTE } from "../../../routes";
import {
  useAddOrderMutation,
  useLazyGetOrderByIdQuery,
} from "../../../services/orderService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { useToastMessage } from "../../../shared/hooks/useToastMessage";
import { RootState } from "../../../store";
import { resetCart } from "../../../store/cart/cartSlice";
import {
  comment,
  deliveryAddress,
  orderFormValidation,
} from "../../order/form/fields";
import { OrderFormFields } from "../../order/types";

import CartProduct from "./CartProduct";

const Cart = () => {
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const { products: productsInCart } = useSelector(
    (state: RootState) => state.cart
  );

  const [addOrder, { isLoading, isSuccess, error, data }] =
    useAddOrderMutation();
  const [getOrder, { data: newOrderData }] = useLazyGetOrderByIdQuery();

  useToastMessage({
    isSuccess,
    error,
    successMessage: "Order placed successfully",
    errorMessage: "There was an error when placing the order",
  });

  const methods = useForm<OrderFormFields>({
    // @ts-ignore
    resolver: yupResolver(orderFormValidation),
    mode: "onSubmit",
  });

  const { handleSubmit, getValues } = methods;

  const onSubmit = () => {
    const orderParams = productsInCart.map((product) => ({
      id: product.id,
      quantity: product.quantity,
    }));
    // placing order
    addOrder({ ...getValues(), products: orderParams });
  };

  useEffect(() => {
    if (isSuccess && data) {
      const newCreatedOrderId = data[0].entityId;
      getOrder(newCreatedOrderId);
      dispatch(resetCart());
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSuccess, data]);

  useEffect(() => {
    if (newOrderData && isSuccess) {
      // order delivery notification
      toast.success(
        `Order will be shipped on ${dayjs(newOrderData.deliveryOnUtc).format(
          "DD/MM/YYYY HH:mm"
        )}`,
        { autoClose: 10000 }
      );
      // navigate to products after getting the new order from BE
      navigate(PRODUCTS_ROUTE);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [newOrderData, isSuccess]);

  return (
    <Paper sx={{ p: 2 }}>
      <Box
        sx={{
          display: "flex",
          justifyContent: "center",
          alignItems: "center",
          flexDirection: "column",
          minHeight: "84vh",
        }}
      >
        <Typography variant="h6">My Cart</Typography>
        {!productsInCart?.length ? (
          <img
            src={"https://www.buy.airoxi.com/img/empty-cart-1.png"}
            alt={"empty cart"}
          />
        ) : (
          <>
            <List sx={{ width: "100%", maxWidth: 500 }}>
              {productsInCart?.map((product) => (
                <CartProduct key={product.id} product={product} />
              ))}
            </List>
            <FormProvider {...methods}>
              <Stack
                component="form"
                sx={{
                  width: "50ch",
                  mt: 2,
                }}
                spacing={2}
                noValidate
                autoComplete="off"
                onSubmit={handleSubmit(onSubmit)}
              >
                <Input field={deliveryAddress} label={"Delivery Address"} />
                <Input
                  field={comment}
                  label={"Additional Comment"}
                  multiline
                  rows={2}
                  maxRows={4}
                />

                <SubmitButton isLoading={isLoading}>Order</SubmitButton>
              </Stack>
            </FormProvider>
          </>
        )}
      </Box>
    </Paper>
  );
};

export default Cart;
