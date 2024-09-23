import { lazy, Suspense } from "react";
import { RouteObject } from "react-router-dom";
import { pathKeys } from "../config.route";
import EventDetailSkeleton from "./EventDetail.skeleton";

const EventDetail = lazy(() => import("./EventDetail"));

export const EventDetailRoute: RouteObject = {
    path: pathKeys.event_detail(),
    element: (
        <Suspense fallback={<EventDetailSkeleton />}>
            <EventDetail />
        </Suspense>
    ),  
};