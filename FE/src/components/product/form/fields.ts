import * as yup from "yup";

enum ProductFields {
  name = "name",
  price = "price",
  amount = "capacity",
  description = "description",
  image = "image",
}

export const { name, price, amount, description, image } = ProductFields;

export const productFormValidation = yup
  .object({
    [name]: yup.string().required(),
    [price]: yup
      .number()
      .transform((value) => (isNaN(value) ? undefined : value))
      .nullable()
      .min(1)
      .required(),
    [amount]: yup
      .number()
      .transform((value) => (isNaN(value) ? undefined : value))
      .nullable()
      .min(1)
      .required(),
    [description]: yup.string().required(),
  })
  .required();
