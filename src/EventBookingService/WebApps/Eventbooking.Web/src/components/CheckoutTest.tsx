import { loadStripe } from "@stripe/stripe-js";
import { useAuth } from "react-oidc-context";

const stripe = await loadStripe("pk_test_51PcP70RoqHqSv3QAAupWntHZsbcVDouqhF2b3UvdCPDpsOeeVc8PPQ5V0Gdn28c0sGjmcK1Jgr00uPrH9HpmzIA400lhi2S3Fx");

const CheckoutTest = () => {

    const auth = useAuth();
    const accessToken = auth.user?.access_token;

    const checkout = async () => {
        const res = await fetch('https://localhost:5056/checkout', {
            method: 'POST', 
            headers: {
                'Authorization': `Bearer ${accessToken}`,
                'Content-Type': 'application/json'
            }
        });

        const body = await res.json();
        console.log(body);
        stripe?.redirectToCheckout({sessionId: body});
    }

    return (
        <>
            <button onClick={checkout}>Checkout</button>
        </>
    );
};

export default CheckoutTest;