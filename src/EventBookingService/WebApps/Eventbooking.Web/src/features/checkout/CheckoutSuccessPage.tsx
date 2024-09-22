import { Button } from "@/components/ui/button"
import { CheckCircle } from "lucide-react"
import { Link } from "react-router-dom"

const CheckoutSuccessPage: React.FC = () => {
  return (
    <div className="min-h-screen bg-gray-50 flex flex-col justify-center items-center py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8">
        <div className="text-center">
          <CheckCircle className="mx-auto h-16 w-16 text-green-500" />
          <h2 className="mt-6 text-3xl font-extrabold text-gray-900">Payment Successful!</h2>
          <p className="mt-2 text-sm text-gray-600">
            Thank you for your purchase. Your booking has been confirmed.
          </p>
        </div>

        <div className="mt-8 space-y-4">
          <Button asChild className="w-full">
            <Link to="/booking">View My Booking</Link>
          </Button>
          <Button variant="outline" asChild className="w-full">
            <Link to="/">Return to Homepage</Link>
          </Button>
        </div>
      </div>
    </div>
  )
}

export default CheckoutSuccessPage