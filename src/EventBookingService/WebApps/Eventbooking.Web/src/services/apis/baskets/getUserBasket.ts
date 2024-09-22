import { axiosPrivate } from "../shared/axiosPrivate"
import { EventCart } from "./types"

export const getUserBasket = async () => {
    return (await axiosPrivate.get<EventCart>("/basket-service/basket")).data
}