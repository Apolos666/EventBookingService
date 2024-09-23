import React from 'react'
import { Skeleton } from "@/components/ui/skeleton"
import { CheckCircle } from "lucide-react"

const CheckoutSuccessSkeleton: React.FC = () => {
  return (
    <div className="min-h-screen bg-gray-50 flex flex-col justify-center items-center py-12 px-4 sm:px-6 lg:px-8">
      <div className="max-w-md w-full space-y-8">
        <div className="text-center">
          <CheckCircle className="mx-auto h-16 w-16 text-gray-300 animate-pulse" />
          <Skeleton className="h-9 w-3/4 mx-auto mt-6" />
          <Skeleton className="h-4 w-5/6 mx-auto mt-2" />
        </div>

        <div className="mt-8 space-y-4">
          <Skeleton className="h-10 w-full" />
          <Skeleton className="h-10 w-full" />
        </div>
      </div>
    </div>
  )
}

export default CheckoutSuccessSkeleton