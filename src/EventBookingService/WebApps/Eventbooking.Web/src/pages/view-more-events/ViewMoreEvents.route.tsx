import { lazy, Suspense } from "react";
import { RouteObject } from "react-router-dom";
import { pathKeys } from "../config.route";
import ViewMoreEventsSkeleton from "./ViewMoreEvents.skeleton";

const ViewMoreEvents = lazy(() => import("./ViewMoreEvents"));

export const ViewMoreEventsRoute: RouteObject = {
    path: pathKeys.view_more_events(),
    element: (
        <Suspense fallback={<ViewMoreEventsSkeleton />}>
            <ViewMoreEvents />
        </Suspense>
    ),  
};