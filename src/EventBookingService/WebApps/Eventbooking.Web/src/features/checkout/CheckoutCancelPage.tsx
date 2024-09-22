import React from 'react'
import { Button } from "@/components/ui/button"
import { XCircle } from "lucide-react"
import { Link } from 'react-router-dom'

const CheckoutCancelPage: React.FC = () => {
  return (
    <div className="min-h-screen bg-gray-50 flex flex-col justify-center py-12 sm:px-6 lg:px-8">
      <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
        <div className="bg-white py-8 px-4 shadow sm:rounded-lg sm:px-10">
          <div className="text-center">
            <XCircle className="mx-auto h-12 w-12 text-red-500" />
            <h2 className="mt-6 text-3xl font-extrabold text-gray-900">Checkout Cancelled</h2>
            <p className="mt-2 text-sm text-gray-600">
              Your order has been cancelled. No charges have been made.
            </p>
          </div>

          <div className="mt-8">
            <div className="space-y-4">
              <div>
                <h3 className="text-lg font-medium text-gray-900">What happened?</h3>
                <p className="mt-1 text-sm text-gray-600">
                  Your checkout process was cancelled. This could be due to:
                </p>
                <ul className="mt-2 text-sm text-gray-600 list-disc list-inside">
                  <li>You chose to cancel the transaction</li>
                  <li>There was an issue with the payment processing</li>
                  <li>The session timed out</li>
                </ul>
              </div>
            </div>
          </div>

          <div className="mt-6 flex flex-col space-y-4">
            <Button asChild>
              <Link to="/cart">Return to Cart</Link>
            </Button>
            <Button variant="outline" asChild>
              <Link to="/">Continue Shopping</Link>
            </Button>
          </div>
        </div>
      </div>
    </div>
  )
}

export default CheckoutCancelPage