import { ListQueryParams } from "../../../services/types";
import { Entity, Image } from "../../../shared/types";

export interface Product extends Entity {
  name: string;
  price: number;
  capacity: number;
  description: string;
  productImage: Image;
}

export interface ProductQueryParams extends ListQueryParams {
  onlyActive: boolean;
  isDealer?: boolean;
}

export interface ProductFormFields extends Omit<Product, "id"> {}
