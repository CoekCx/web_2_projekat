import {
  Order,
  OrderFormFields,
  OrderResponse,
} from "../components/order/types";

import { api, orderTagType, productTagType } from "./service";
import { ListQueryParams, ListResponse } from "./types";

interface OrderListQueryParams extends ListQueryParams {
  newOrders?: boolean;
}

export const orderService = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getOrders: builder.query<
      { data: OrderResponse[]; totalCount: number },
      OrderListQueryParams
    >({
      query: ({ newOrders, ...params }: OrderListQueryParams) => ({
        url: newOrders ? `/api/order/orders/shipping` : `/api/order/orders`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<OrderResponse>) => ({
        data: returnValue.items,
        totalCount: returnValue.totalCount,
      }),
      providesTags: (result) =>
        result
          ? [
              ...result.data.map(({ id }) => ({
                type: "Order" as const,
                id,
              })),
              orderTagType,
            ]
          : [orderTagType],
    }),
    getOrderById: builder.query<Order, string | undefined>({
      query: (id) => `/api/order/orders/${id}`,
      providesTags: (result, error, id) => [{ type: "Order", id }],
    }),
    // MUTATIONS
    addOrder: builder.mutation<{ entityId: string }[], OrderFormFields>({
      query: (order: OrderFormFields) => ({
        url: `/api/order/orders`,
        method: "POST",
        body: order,
      }),
      invalidatesTags: [orderTagType, productTagType],
    }),
    updateOrder: builder.mutation<
      Order,
      { id: string | undefined; order: OrderFormFields }
    >({
      query: ({ id, order }) => ({
        url: `/api/order/orders/${id}`,
        method: "PUT",
        body: order,
      }),
      invalidatesTags: (result, error, arg) => [{ type: "Order", id: arg.id }],
    }),
    cancelOrder: builder.mutation<Order, string>({
      query: (id) => ({
        url: `/api/order/orders/${id}`,
        method: "DELETE",
      }),
      invalidatesTags: (result, error, arg) => [{ type: "Order", id: arg }],
    }),
  }),
});

export const {
  useGetOrdersQuery,
  useAddOrderMutation,
  useLazyGetOrderByIdQuery,
  useCancelOrderMutation,
} = orderService;
