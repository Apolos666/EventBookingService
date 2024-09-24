export type BookingDto = {
    id: string;
    userId: string;
    createdAt: Date;
    bookingStatus: string;
    totalQuantity: number;
    totalPrice: number;
    bookingItems: BookingItemDto[];
}

export type BookingItemDto = {
    bookingId: string;
    eventId: string;
    startDateTime: Date;
    eventLocationId: string;    
    eventLocationName: string;
    eventName: string;
    quantity: number;
    price: number;  
    totalPrice: number;
    code: string;
}