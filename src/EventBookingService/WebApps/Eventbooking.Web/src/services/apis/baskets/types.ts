export interface EventCartItem {
    id: string; 
    eventId: string; 
    startDateTime: Date; 
    eventLocationId: string; 
    eventName: string;
    quantity: number;
    price: number;
}

export interface EventCart {
    userId: string; 
    createdAt: Date; 
    updatedAt: Date; 
    items: EventCartItem[];
    totalPrice: number;
}
