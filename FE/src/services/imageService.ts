import { Entity } from "../shared/types";

import { api, imageTagType } from "./service";

interface Image extends Entity {
  url: string;
}

export const imageService = api.injectEndpoints({
  endpoints: (builder) => ({
    // QUERIES
    getImageById: builder.query<FormData, string | undefined>({
      query: (id) => `/api/storage/images/${id}`,
      providesTags: (result, error, id) => [{ type: "Image", id }],
    }),
    // MUTATIONS
    addImage: builder.mutation<Image, FormData>({
      query: (image: FormData) => ({
        url: `/api/storage/images`,
        method: "POST",
        body: image,
      }),
      invalidatesTags: [imageTagType],
    }),
  }),
});

export const { useAddImageMutation } = imageService;
