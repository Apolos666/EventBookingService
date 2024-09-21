import { Event } from "@/features/events/shared/event.types"
import { axiosInstance } from "../shared/axiosInstance"
import { paginationRequest } from "../shared/types"

type EventsResponse = {
    events: Event[]
}

export const getEvents = async ({pageNumber = 1, pageSize = 10} : paginationRequest) => {
    return (await axiosInstance.get<EventsResponse>(`event-service/events?pageNumber=${pageNumber}&pageSize=${pageSize}`)).data
}