import { lazy, Suspense } from "react";
import { RouteObject } from "react-router-dom";
import { pathKeys } from "../config.route";
import CheckoutSuccessSkeleton from "./CheckoutSuccess.skeleton";
import CheckoutCancelSkeleton from "./CheckoutCancel.skeleton";

const CheckoutSuccess = lazy(() => import("./CheckoutSuccess"));
const CheckoutCancel = lazy(() => import("./CheckoutCancel"));

export const CheckoutRoute: RouteObject = {
    path: pathKeys.checkout.root,
    children: [
        {
            path: pathKeys.checkout.success(),
            element: (
                <Suspense fallback={<CheckoutSuccessSkeleton />}>
                    <CheckoutSuccess />
                </Suspense>
            )
        },
        {
            path: pathKeys.checkout.canceled(),
            element: (
                <Suspense fallback={<CheckoutCancelSkeleton />}>
                    <CheckoutCancel />
                </Suspense>
            )
        }
    ]
};