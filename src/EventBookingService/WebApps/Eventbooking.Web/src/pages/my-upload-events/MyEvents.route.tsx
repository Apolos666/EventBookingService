import { lazy, Suspense } from "react";
import { RouteObject } from "react-router-dom";
import { pathKeys } from "../config.route";
import MyEventsSkeleton from "./MyEvents.skeleton";

const MyEvents = lazy(() => import("./MyEvents"));

export const MyEventsRoute: RouteObject = {
    path: pathKeys.my_uploaded_events(),
    element: (
        <Suspense fallback={<MyEventsSkeleton />}>
            <MyEvents />
        </Suspense>
    ),  
};