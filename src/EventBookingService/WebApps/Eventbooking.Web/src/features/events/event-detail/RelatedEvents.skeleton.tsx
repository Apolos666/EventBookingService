import { Skeleton } from "@/components/ui/skeleton"
import EventCardSkeleton from "../shared/EventCard.skeleton"

const RelatedEventsSkeleton: React.FC = () => {
    return (
        <section className="mt-12">
            <Skeleton className="h-8 w-64 mb-6" />
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
                {Array.from({ length: 3 }).map((_, index) => (
                    <EventCardSkeleton key={index} />
                ))}
            </div>
        </section>
    )
}

export default RelatedEventsSkeleton;