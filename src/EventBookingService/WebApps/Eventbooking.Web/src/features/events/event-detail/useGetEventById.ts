import { getEventById } from "@/services/apis/events/getEventById";
import { useQuery } from "@tanstack/react-query";

export function useGetEventById(eventId: string) {
    return useQuery({
        queryKey: ["event", eventId],
        queryFn: () => getEventById(eventId),
    })
}