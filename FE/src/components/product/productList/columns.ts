import { Product } from "../types";

export const productColumns = [
  {
    id: "name",
    label: "Name",
  },
  {
    id: "price",
    label: "Price",
    format: (row: Product) => `$${row.price},00`,
  },
  {
    id: "description",
    label: "Description",
  },
];
