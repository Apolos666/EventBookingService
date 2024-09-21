import { axiosPrivate } from "../shared/axiosPrivate"

export interface LocationDto {
    name: string;
    address: string;
    city: string;
    state: string;
    zipCode: string;
    country: string;
}

export interface EventLocationDto {
    location: LocationDto;
    maxAttendees: number;
    price: number;
}

export interface EventDto {
    name: string;
    description: string;
    startDateTime: Date;
    endDateTime: Date;
    eventLocationDtos: EventLocationDto[];
}

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