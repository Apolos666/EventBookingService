import { lazy, Suspense } from "react";
import { RouteObject } from "react-router-dom";
import { pathKeys } from "../config.route";
import HomepageSkeleton from "./HomePage.skeleton";

const HomePage = lazy(() => import("./HomePage"));

export const HomePageRoute: RouteObject = {
    path: pathKeys.home(),
    element: (
        <Suspense fallback={<HomepageSkeleton />}>
            <HomePage />
        </Suspense>
    ),  
};