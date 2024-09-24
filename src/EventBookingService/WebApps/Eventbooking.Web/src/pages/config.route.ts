export const pathKeys = {
    root: "/",
    home: () => pathKeys.root,
    view_more_events: () => pathKeys.root.concat("events"),
    event_detail: () => pathKeys.root.concat("events/:id"),
    checkout: {
        root: "/checkout",
        success: () => pathKeys.checkout.root.concat("/success"),
        canceled: () => pathKeys.checkout.root.concat("/canceled"),
    },
    bookings: () => pathKeys.root.concat("bookings"),
}