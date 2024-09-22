import { EventCart } from "@/features/shopping-cart/shopping-cart.types"
import { axiosPrivate } from "../shared/axiosPrivate"

export interface UserCartResponse {
    cart: EventCart
}

export const getUserBasket = async () => {
    return (await axiosPrivate.get<UserCartResponse>("/basket-service/baskets")).data
}