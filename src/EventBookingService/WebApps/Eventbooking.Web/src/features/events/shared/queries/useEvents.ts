import { getEvents } from "@/services/apis/events/getEvents";
import { paginationRequest } from "@/services/apis/shared/types";
import { keepPreviousData, useQuery } from "@tanstack/react-query"

export function useEvents({pageNumber = 1, pageSize = 10} : paginationRequest) {
    return useQuery({
        queryKey: ["events", { pageNumber, pageSize }],
        queryFn: () => getEvents({pageNumber, pageSize}),
        placeholderData: keepPreviousData
    });
}