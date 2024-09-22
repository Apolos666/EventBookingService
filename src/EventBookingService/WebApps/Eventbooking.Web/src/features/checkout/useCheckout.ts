import { checkout } from "@/services/apis/payments/checkout";
import { loadStripe } from "@stripe/stripe-js";
import { useMutation } from "@tanstack/react-query";

const stripe = await loadStripe("pk_test_51PcP70RoqHqSv3QAAupWntHZsbcVDouqhF2b3UvdCPDpsOeeVc8PPQ5V0Gdn28c0sGjmcK1Jgr00uPrH9HpmzIA400lhi2S3Fx");

export function useCheckout() {
    return useMutation({
        mutationFn: () => checkout(),
        onSuccess(data) {
            stripe?.redirectToCheckout({sessionId: data});
        },
    })
}