import { Entity } from "../../../shared/types";
import { Product } from "../../product/types";
import { User } from "../../user/types";

export enum OrderStatus {
  Shipping,
  Finished,
  Cancelled,
}

export interface Order extends Entity {
  name: string;
  comment: string;
  deliveryAddress: string;
  status: OrderStatus;
  deliveryPrice: number;
  fullPrice: number;
  deliveryOnUtc: Date;
}

export interface OrderResponse extends Order {
  dealerId: User;
  productDetailsResponses: Product[];
}

export interface ProductInOrder extends Entity {
  quantity: number;
}

export interface OrderFormFields
  extends Omit<
    Order,
    "id" | "name" | "status" | "fullPrice" | "deliveryOnUtc"
  > {
  products: ProductInOrder[];
  comment: "string";
  deliveryAddress: "string";
}
