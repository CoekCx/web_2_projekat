import {
  Product,
  ProductFormFields,
  ProductQueryParams,
} from "../components/product/types";

import { api, dealerProductTagType, productTagType } from "./service";
import { ListResponse } from "./types";

export const productApi = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getProducts: builder.query<
      { data: Product[]; totalCount: number },
      ProductQueryParams
    >({
      query: (params: ProductQueryParams) => ({
        url: params.isDealer
          ? `/api/catalog/product/creator`
          : `/api/catalog/product`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<Product>) => ({
        data: returnValue.items,
        totalCount: returnValue.totalCount,
      }),
      providesTags: (result, error, arg) =>
        result
          ? [
              ...result.data.map(({ id }) => ({
                type: arg.isDealer
                  ? ("DealerProduct" as const)
                  : ("Product" as const),
                id,
              })),
              arg.isDealer ? dealerProductTagType : productTagType,
            ]
          : [arg.isDealer ? dealerProductTagType : productTagType],
    }),
    getProductById: builder.query<Product, string | undefined>({
      query: (id) => `/api/catalog/product/${id}`,
      providesTags: (result, error, id) => [{ type: "Product", id }],
    }),
    // MUTATIONS
    addProduct: builder.mutation<Product, ProductFormFields>({
      query: (product: ProductFormFields) => ({
        url: `/api/catalog/product`,
        method: "POST",
        body: product,
      }),
      invalidatesTags: [productTagType, dealerProductTagType],
    }),
    updateProduct: builder.mutation<
      Product,
      { id: string | undefined; product: ProductFormFields }
    >({
      query: ({ id, product }) => ({
        url: `/api/catalog/product/${id}`,
        method: "PUT",
        body: product,
      }),
      invalidatesTags: (result, error, arg) => [
        { type: "Product", id: arg.id },
      ],
    }),
    deleteProduct: builder.mutation<Product, string | undefined>({
      query: (id) => ({
        url: `/api/catalog/product/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: (result, error, arg) => [{ type: "Product", id: arg }],
    }),
    uploadProductImage: builder.mutation<
      string,
      { imageId: string | undefined; productId?: string | undefined }
    >({
      query: ({ imageId, productId }) => ({
        url: `/api/catalog/product/product-image`,
        method: "PUT",
        body: { imageId, productId },
      }),
      invalidatesTags: (result, error, arg) => [{ type: "Product", arg }],
    }),
  }),
});

export const {
  useUpdateProductMutation,
  useAddProductMutation,
  useGetProductByIdQuery,
  useGetProductsQuery,
  useDeleteProductMutation,
  useUploadProductImageMutation,
} = productApi;
