import { Button } from "@/components/ui/button"
import { Input } from "@/components/ui/input"
import { Skeleton } from "@/components/ui/skeleton"
import { SearchIcon } from "lucide-react"
import EventCardSkeleton from '@/features/events/shared/EventCard.skeleton'

const HomepageSkeleton: React.FC = () => {
  return (
    <main className="container mx-auto px-4 py-8">
      <section className="text-center mb-12">
        <Skeleton className="h-10 w-3/4 mx-auto mb-4" />
        <Skeleton className="h-6 w-2/3 mx-auto mb-6" />
        <div className="flex max-w-md mx-auto">
          <Input
            type="text"
            placeholder="Search events..."
            className="rounded-r-none"
            disabled
          />
          <Button className="rounded-l-none" disabled>
            <SearchIcon className="h-4 w-4 mr-2" />
            Search
          </Button>
        </div>
      </section>

      <section>
        <Skeleton className="h-8 w-48 mb-4" />
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {Array.from({ length: 3 }).map((_, index) => (
            <EventCardSkeleton key={index} />
          ))}
        </div>
        <div className="text-center mt-8">
          <Skeleton className="h-10 w-40 mx-auto" />
        </div>
      </section>
    </main>
  )
}

export default HomepageSkeleton