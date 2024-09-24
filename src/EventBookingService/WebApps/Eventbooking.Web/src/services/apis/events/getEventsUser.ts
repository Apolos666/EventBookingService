import { Event } from '../../../features/events/shared/event.types';
import { axiosPrivate } from '../shared/axiosPrivate';

export type EventUserResponse = {
    events: Event[];
}

export const getEventsUser = async () => {
    return (await axiosPrivate.get<EventUserResponse>('/event-service/events/user')).data;
}