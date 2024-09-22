import { Skeleton } from "@/components/ui/skeleton"
import { Card, CardContent } from "@/components/ui/card"

const EventSidebarSkeleton: React.FC = () => {
  return (
    <div className="w-full lg:w-1/3">
      <Card className="p-6 sticky top-4">
        <CardContent className="p-0">
          <Skeleton className="h-6 w-1/2 mb-4" />
          <Skeleton className="h-10 w-full mb-4" />
          <div className="flex items-center justify-between mb-6">
            <Skeleton className="h-6 w-1/4" />
            <div className="flex items-center">
              <Skeleton className="h-8 w-8 rounded" />
              <Skeleton className="h-6 w-8 mx-4" />
              <Skeleton className="h-8 w-8 rounded" />
            </div>
          </div>
          <Skeleton className="h-10 w-full" />
        </CardContent>
      </Card>
    </div>
  )
}

export default EventSidebarSkeleton