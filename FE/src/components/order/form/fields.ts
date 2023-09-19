import * as yup from "yup";

enum OrderFields {
  quantity = "quantity",
  comment = "comment",
  deliveryAddress = "deliveryAddress",
}

export const { quantity, comment, deliveryAddress } = OrderFields;

export const orderFormValidation = yup
  .object({
    [comment]: yup.string(),
    [deliveryAddress]: yup.string().required(),
  })
  .required();
