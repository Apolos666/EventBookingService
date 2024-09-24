import { getEventsUser } from "@/services/apis/events/getEventsUser";
import { useQuery } from "@tanstack/react-query";

export function useGetEventsUser() {
    return useQuery({
        queryKey: ["events_user"],
        queryFn: () => getEventsUser(),
    })
}