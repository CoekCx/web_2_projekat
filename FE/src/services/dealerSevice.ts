import { User } from "../components/user/types";

import { api, dealerTagType, pendingDealerTagType } from "./service";
import { DealerQueryParams, ListResponse } from "./types";

export const dealerService = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getDealers: builder.query<
      { data: User[]; totalCount: number },
      DealerQueryParams
    >({
      query: ({ pending, ...params }: DealerQueryParams) => ({
        url: `/api/administration/dealer/${pending ? "pending" : ""}`,
        method: "GET",
        params,
      }),
      transformResponse: (returnValue: ListResponse<User>) => ({
        data: returnValue.items,
        totalCount: returnValue.totalCount,
      }),
      providesTags: (result, error, args) =>
        result
          ? [
              ...result.data.map(({ id }) => ({
                type: args.pending
                  ? ("PendingDealer" as const)
                  : ("Dealer" as const),
                id,
              })),
            ]
          : [dealerTagType, pendingDealerTagType],
    }),
    // MUTATIONS
    approveRequest: builder.mutation<User, string | undefined>({
      query: (id) => ({
        url: `/api/administration/dealer/approve/${id}`,
        method: "PUT",
      }),
      invalidatesTags: (result, error, arg) => [
        { type: "Dealer", id: arg },
        { type: "PendingDealer", id: arg },
      ],
    }),
    disapproveRequest: builder.mutation<User, string | undefined>({
      query: (id) => ({
        url: `/api/administration/dealer/disapprove/${id}`,
        method: "PUT",
      }),
      invalidatesTags: (result, error, arg) => [
        { type: "Dealer", id: arg },
        { type: "PendingDealer", id: arg },
      ],
    }),
  }),
});

export const {
  useGetDealersQuery,
  useApproveRequestMutation,
  useDisapproveRequestMutation,
} = dealerService;
