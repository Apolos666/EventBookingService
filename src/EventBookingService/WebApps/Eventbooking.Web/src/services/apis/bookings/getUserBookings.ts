import { axiosPrivate } from "../shared/axiosPrivate"
import { BookingDto } from "./types"

export type UserBookingResponse = {
    bookings: BookingDto[];
}
    
export const getUserBookings = async () => {
    return (await axiosPrivate.get<UserBookingResponse>("/booking-service/bookings/user")).data
}