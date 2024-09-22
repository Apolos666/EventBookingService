import { axiosPrivate } from "../shared/axiosPrivate"

export const checkout = async () => {
    return (await axiosPrivate.post<string>("/payment-service/checkout")).data;
}