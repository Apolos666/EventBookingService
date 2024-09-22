import { axiosInstance } from "../shared/axiosInstance"
import { Event } from "../../../features/events/shared/event.types"

type EventResponse = {
    event: Event
}

export const getEventById = async (eventId: string) => {
    return (await axiosInstance.get<EventResponse>(`event-service/events/${eventId}`)).data
}