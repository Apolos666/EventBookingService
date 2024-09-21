export type Event = {
    id: string;
    hostId: string; 
    name: string;
    description: string;
    eventImageUrl: string;
    startDateTime: Date;
    endDateTime: Date;
    eventLocations: EventLocation[];
    userRegistedId: string[]; 
}

export type EventLocation = {
    id: string; 
    location: Location;
    maxAttendees: number;
    registeredAttendees: number;
    price: number; 
}

export type Location = {
    name: string;
    address: string;
    city: string;
    state: string;
    zipCode: string;
    country: string;
}