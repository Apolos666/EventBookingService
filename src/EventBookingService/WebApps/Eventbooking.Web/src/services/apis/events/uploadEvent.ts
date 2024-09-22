import { axiosPrivate } from "../shared/axiosPrivate"
import { EventDto } from "./types";

export const uploadEvent = async (event: EventDto, image: File) => {
    const formData = new FormData();
    formData.append('image', image);
    formData.append('event', JSON.stringify(event));

    return (await axiosPrivate.post<string>('event-service/events', formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    })).data;
}