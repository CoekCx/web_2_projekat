import { FC } from "react";
import { useDispatch } from "react-redux";

import CancelPresentationIcon from "@mui/icons-material/CancelPresentation";
import { Button, ListItem, ListItemText } from "@mui/material";

import { removeFromCart, StoreProduct } from "../../../store/cart/cartSlice";
interface CartProductProps {
  product: StoreProduct;
}

const CartProduct: FC<CartProductProps> = ({ product }) => {
  const { name, quantity, price } = product;

  const dispatch = useDispatch();

  const handleRemoveProductFromCart = () => {
    dispatch(removeFromCart(product.id));
  };

  return (
    <ListItem
      alignItems="center"
      sx={{
        display: "flex",
        justifyContent: "space-between",
        alignItems: "center",
        backgroundColor: "#ededed",
        my: 1,
        borderRadius: "4px",
      }}
    >
      <ListItemText
        primary={`Product: ${name}`}
        secondary={
          <>
            <span>{` Quantity: ${quantity}`}</span>,
            <span>{` Total Price: $${price}, 00`}</span>
          </>
        }
      />
      <Button color={"error"} onClick={handleRemoveProductFromCart}>
        <CancelPresentationIcon />
      </Button>
    </ListItem>
  );
};

export default CartProduct;
