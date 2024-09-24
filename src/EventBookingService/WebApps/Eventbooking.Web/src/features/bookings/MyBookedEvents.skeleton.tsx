import { Skeleton } from "@/components/ui/skeleton";

const MyBookedEventsSkeleton: React.FC = () => (
    <div className="container mx-auto py-8">
        <Skeleton className="h-10 w-64 mb-6" />
        <div className="space-y-4">
            {[...Array(5)].map((_, i) => (
                <Skeleton key={i} className="h-16 w-full" />
            ))}
        </div>
    </div>
)

export default MyBookedEventsSkeleton;