import { EventCart } from "@/features/shopping-cart/shopping-cart.types";
import { axiosPrivate } from "../shared/axiosPrivate";
import { EventCartDto } from "./types";

export const postUserBasket = async (event: EventCartDto) => {
    return (await axiosPrivate.post<EventCart>("/basket-service/baskets", {
        cart: event
    })).data;
}