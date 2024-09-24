export const pathKeys = {
    root: "/",
    home: () => pathKeys.root,
    view_more_events: () => pathKeys.root.concat("events"),
    event_details: {
        set_up: () => pathKeys.root.concat("events/:id"), 
        get_detail_path: (id: string) => pathKeys.root.concat(`events/${id}`), 
    },
    checkout: {
        root: "/checkout",
        success: () => pathKeys.checkout.root.concat("/success"),
        canceled: () => pathKeys.checkout.root.concat("/canceled"),
    },
    bookings: () => pathKeys.root.concat("bookings"),
    my_uploaded_events: () => pathKeys.root.concat("my-events"),
}