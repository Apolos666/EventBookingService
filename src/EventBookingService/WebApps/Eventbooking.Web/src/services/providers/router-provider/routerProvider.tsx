import { MainLayout } from "@/layouts/MainLayout";
import { TempLayout } from "@/layouts/TempLayout";
import { BookingsPageRoute } from "@/pages/bookings/BookingsPage.route";
import { CheckoutRoute } from "@/pages/checkout/Checkout.route";
import { EventDetailRoute } from "@/pages/event-detail/EventDetail.route";
import { HomePageRoute } from "@/pages/home/HomePage.route";
import { ViewMoreEventsRoute } from "@/pages/view-more-events/ViewMoreEvents.route";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

export function BrowserRouter() {
    return <RouterProvider router={browserRouter} />
}

const browserRouter = createBrowserRouter([
    {
        errorElement: <div>Not Found</div>,
        children: [
            {
                element: <MainLayout />,
                children: [HomePageRoute, ViewMoreEventsRoute, EventDetailRoute, BookingsPageRoute]
            },
            {
                element: <TempLayout />,
                children: [CheckoutRoute]
            }
        ]
    }
])