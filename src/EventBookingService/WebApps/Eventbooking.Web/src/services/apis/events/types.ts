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
    refundPolicy: string;
    aboutThisEvent: string;
    startDateTime: Date;
    endDateTime: Date;
    eventLocationDtos: EventLocationDto[];
}