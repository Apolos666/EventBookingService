import React from 'react'
import { Skeleton } from "@/components/ui/skeleton"
import { XCircle } from "lucide-react"

const CheckoutCancelSkeleton: React.FC = () => {
  return (
    <div className="min-h-screen bg-gray-50 flex flex-col justify-center py-12 sm:px-6 lg:px-8">
      <div className="mt-8 sm:mx-auto sm:w-full sm:max-w-md">
        <div className="bg-white py-8 px-4 shadow sm:rounded-lg sm:px-10">
          <div className="text-center">
            <XCircle className="mx-auto h-12 w-12 text-gray-300 animate-pulse" />
            <Skeleton className="h-9 w-3/4 mx-auto mt-6" />
            <Skeleton className="h-4 w-5/6 mx-auto mt-2" />
          </div>

          <div className="mt-8">
            <div className="space-y-4">
              <div>
                <Skeleton className="h-6 w-1/2 mb-2" />
                <Skeleton className="h-4 w-full mb-2" />
                <Skeleton className="h-4 w-5/6" />
                <div className="mt-2 space-y-2">
                  <Skeleton className="h-4 w-3/4" />
                  <Skeleton className="h-4 w-4/5" />
                  <Skeleton className="h-4 w-2/3" />
                </div>
              </div>
            </div>
          </div>

          <div className="mt-6 flex flex-col space-y-4">
            <Skeleton className="h-10 w-full" />
            <Skeleton className="h-10 w-full" />
          </div>
        </div>
      </div>
    </div>
  )
}

export default CheckoutCancelSkeleton