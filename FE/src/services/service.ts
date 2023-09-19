import { createApi } from "@reduxjs/toolkit/query/react";

import config from "./config";

export const dealerTagType = "Dealer";
export const pendingDealerTagType = "PendingDealer";
export const imageTagType = "Image";
export const orderTagType = "Order";
export const productTagType = "Product";
export const dealerProductTagType = "DealerProduct";

export const userTagType = "User";

export const api = createApi({
  ...config,
  keepUnusedDataFor: 30,
  tagTypes: [
    dealerTagType,
    pendingDealerTagType,
    imageTagType,
    orderTagType,
    productTagType,
    dealerProductTagType,
    userTagType,
  ],
  endpoints: () => ({}),
});
