export interface EventCartItemDto {
    eventId: string;
    startDateTime: Date; 
    eventLocationId: string;
    eventLocationName: string; 
    eventName: string;
    quantity: number;
    price: number;
}

export interface EventCartDto {
    createdAt: Date; 
    updatedAt: Date; 
    items: EventCartItemDto[];
}