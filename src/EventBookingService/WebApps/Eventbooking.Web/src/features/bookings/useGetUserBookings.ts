import { getUserBookings } from "@/services/apis/bookings/getUserBookings";
import { useQuery } from "@tanstack/react-query";

export function useGetUserBookings() {
    return useQuery({
        queryKey: ["userBookings"],
        queryFn: () => getUserBookings(),
    })
}