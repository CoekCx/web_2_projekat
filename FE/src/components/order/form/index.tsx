import React, { useEffect } from "react";
import { FormProvider, useForm } from "react-hook-form";
import { useNavigate } from "react-router-dom";
import { toast } from "react-toastify";

import { yupResolver } from "@hookform/resolvers/yup";
import { Stack } from "@mui/material";
import Box from "@mui/material/Box";

import { PRODUCTS_ROUTE } from "../../../routes";
import { useAddOrderMutation } from "../../../services/orderService";
import Input from "../../../shared/form/Input";
import SubmitButton from "../../../shared/form/SubmitButton";
import { OrderFormFields } from "../types";

import { comment, deliveryAddress, orderFormValidation } from "./fields";

const OrderForm = () => {
  const navigate = useNavigate();

  const [order, { isLoading: isSubmitting, error, isSuccess }] =
    useAddOrderMutation();

  const methods = useForm<OrderFormFields>({
    // @ts-ignore
    resolver: yupResolver(orderFormValidation),
    mode: "onSubmit",
  });

  const { handleSubmit } = methods;

  const onSubmit = () => {
    order(methods.getValues());
  };

  useEffect(() => {
    if (error) {
      toast.error("There was an error when trying to place the cart");
    }
  }, [error]);

  useEffect(() => {
    if (isSuccess) {
      toast.success("Order placed successfully");
      navigate(PRODUCTS_ROUTE);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [isSuccess]);

  return (
    <Box
      sx={{
        display: "flex",
        justifyContent: "center",
        alignItems: "center",
        flexDirection: "column",
        minHeight: "100vh",
      }}
    >
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
          <Input field={comment} label={"Comment"} />
          <Input field={deliveryAddress} label={"Delivery Address"} />

          <SubmitButton isLoading={isSubmitting}>Order</SubmitButton>
        </Stack>
      </FormProvider>
    </Box>
  );
};

export default OrderForm;
