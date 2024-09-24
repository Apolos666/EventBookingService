import { lazy, Suspense } from "react";
import { RouteObject } from "react-router-dom";
import { pathKeys } from "../config.route";
import BookingsPageSkeleton from "./BookingsPage.skeleton";

const BookingsPage = lazy(() => import("./BookingsPage"));

export const BookingsPageRoute: RouteObject = {
    path: pathKeys.bookings(),
    element: (
        <Suspense fallback={<BookingsPageSkeleton />}>
            <BookingsPage />
        </Suspense>
    ),  
};