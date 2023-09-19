import { createSlice } from "@reduxjs/toolkit";

import { Product } from "../../components/product/types";

export interface StoreProduct extends Product {
  quantity: number;
}

export type CartState = {
  products: StoreProduct[];
};

const initialState: CartState = {
  products: [],
};

export const cartSlice = createSlice({
  name: "cart",
  initialState,
  reducers: {
    initializeCart: (state, action) => {
      state.products = action.payload;
    },
    addToCart: (state, action) => {
      const addedProductId = action.payload.id;
      const productAlreadyInCart = state.products.find(
        (product) => product.id === addedProductId
      );
      if (productAlreadyInCart) {
        const newProductsState = state.products.map((product) => {
          if (product.id === addedProductId) {
            const newQuantity = product.quantity + action.payload.quantity;
            return { ...product, quantity: newQuantity };
          } else return product;
        });
        localStorage.setItem("cart", JSON.stringify(newProductsState));
        state.products = newProductsState;
      } else {
        const newProductsState = [...state.products, action.payload];
        localStorage.setItem("cart", JSON.stringify(newProductsState));
        state.products = newProductsState;
      }
    },
    removeFromCart: (state, action) => {
      const newProductsState = state.products.filter(
        (product) => product.id !== action.payload
      );
      localStorage.setItem("cart", JSON.stringify(newProductsState));
      state.products = newProductsState;
    },
    resetCart: () => {
      localStorage.removeItem("cart");
      return initialState;
    },
  },
});

export const { initializeCart, addToCart, removeFromCart, resetCart } =
  cartSlice.actions;

export default cartSlice.reducer;
