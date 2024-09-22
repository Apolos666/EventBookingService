import { Card, CardContent, CardFooter } from "@/components/ui/card"
import { Skeleton } from "@/components/ui/skeleton"

const EventCardSkeleton: React.FC = () => {
  return (
    <Card className="overflow-hidden">
      <Skeleton className="h-48 w-full" />
      <CardContent className="p-4">
        <Skeleton className="h-6 w-3/4 mb-2" />
        <div className="space-y-2">
          <div className="flex items-center">
            <Skeleton className="h-4 w-4 mr-2" />
            <Skeleton className="h-4 w-1/4" />
          </div>
          <div className="flex items-center">
            <Skeleton className="h-4 w-4 mr-2" />
            <Skeleton className="h-4 w-1/4" />
          </div>
          <div className="flex items-center">
            <Skeleton className="h-4 w-4 mr-2" />
            <Skeleton className="h-4 w-1/2" />
          </div>
        </div>
        <Skeleton className="h-4 w-full mt-4 mb-2" />
        <Skeleton className="h-4 w-5/6" />
      </CardContent>
      <CardFooter className="p-4">
        <Skeleton className="h-10 w-full" />
      </CardFooter>
    </Card>
  )
}

export default EventCardSkeleton