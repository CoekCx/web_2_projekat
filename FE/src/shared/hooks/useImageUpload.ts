import React, { useCallback, useState } from "react";
import { toast } from "react-toastify";

interface Params {
  addImage: (formData: FormData) => any;
  uploadImage: ({
    imageId,
    productId,
  }: {
    imageId: string;
    productId?: string;
  }) => any;
  productId?: string;
}

export const useImageUpload = ({
  addImage,
  uploadImage,
  productId,
}: Params) => {
  const [imageSrc, setImageSrc] = useState<string | undefined>();

  const handleImageChange = useCallback(
    async (
      event: React.ChangeEvent<HTMLTextAreaElement | HTMLInputElement>
    ) => {
      const file = (event.target as HTMLInputElement).files?.[0];
      if (file) {
        setImageSrc(undefined);
        const formData = new FormData();
        formData.set("imageFile", file);
        // add and upload image
        try {
          const imageAddResult = await addImage(formData).unwrap();
          if (imageAddResult?.id) {
            await uploadImage({
              imageId: imageAddResult?.id,
              ...(productId && { productId }),
            }).unwrap();

            toast.success("Image uploaded successfully");
            setImageSrc(URL.createObjectURL(file));
          }
        } catch (error) {
          toast.error("There was an error when trying to upload image");
        }
      }
    },
      // eslint-disable-next-line react-hooks/exhaustive-deps
      [productId]
  );

  return { handleImageChange, imageSrc, setImageSrc };
};
