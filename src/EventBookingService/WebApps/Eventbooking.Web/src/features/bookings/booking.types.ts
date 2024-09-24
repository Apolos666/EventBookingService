export interface BookingItem {
    startDateTime: string
    eventName: string
    quantity: number
    price: number
    totalPrice: number
    code: string
    eventLocationName: string
}

export interface BookedEvent {
    id: string
    bookingStatus: string
    totalQuantity: number
    totalPrices: number
    createdAt: string
    bookingItems: BookingItem[]
}