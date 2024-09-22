import { Skeleton } from "@/components/ui/skeleton"

const CartItemSkeleton: React.FC = () => {
  return (
    <div className="flex flex-col items-start p-2 w-full">
      <div className="flex justify-between w-full">
        <Skeleton className="h-5 w-2/3" />
        <Skeleton className="h-5 w-1/4" />
      </div>
      <Skeleton className="h-4 w-1/2 mt-1" />
      <Skeleton className="h-4 w-1/3 mt-1" />
      <Skeleton className="h-4 w-1/4 mt-1" />
    </div>
  )
}

export default CartItemSkeleton