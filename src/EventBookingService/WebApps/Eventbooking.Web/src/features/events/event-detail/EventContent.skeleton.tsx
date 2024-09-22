import { Skeleton } from "@/components/ui/skeleton"
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs"

const EventContentSkeleton: React.FC = () => {
  return (
    <div className="w-full lg:w-2/3">
      <Skeleton className="h-10 w-3/4 mb-4" />
      <Skeleton className="w-full h-[400px] rounded-lg mb-6" />
      <Skeleton className="h-4 w-full mb-2" />
      <Skeleton className="h-4 w-5/6 mb-4" />
      
      <div className="grid grid-cols-2 gap-4 mb-6">
        <div>
          <Skeleton className="h-6 w-1/2 mb-2" />
          <Skeleton className="h-5 w-3/4" />
        </div>
        <div>
          <Skeleton className="h-6 w-1/2 mb-2" />
          <Skeleton className="h-5 w-3/4" />
        </div>
      </div>
      
      <Skeleton className="h-6 w-1/4 mb-2" />
      <Tabs defaultValue="0">
        <TabsList>
          {[...Array(3)].map((_, index) => (
            <TabsTrigger key={index} value={index.toString()}>
              <Skeleton className="h-4 w-20" />
            </TabsTrigger>
          ))}
        </TabsList>
        <TabsContent value="0">
          <Skeleton className="h-5 w-full mb-2" />
          <Skeleton className="h-4 w-3/4" />
        </TabsContent>
      </Tabs>
      
      {['Refund Policy', 'About this event'].map((title, index) => (
        <div key={index} className="mb-6">
          <Skeleton className="h-6 w-1/3 mb-2" />
          <Skeleton className="h-4 w-full mb-2" />
          <Skeleton className="h-4 w-5/6" />
        </div>
      ))}
      
      <Skeleton className="h-6 w-1/3 mb-2" />
      <div className="flex items-center">
        <Skeleton className="h-12 w-12 rounded-full mr-4" />
        <div>
          <Skeleton className="h-5 w-40 mb-1" />
          <Skeleton className="h-4 w-60" />
        </div>
      </div>
    </div>
  )
}

export default EventContentSkeleton