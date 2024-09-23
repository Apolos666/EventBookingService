import { Skeleton } from "@/components/ui/skeleton"
import { Link } from 'react-router-dom'
import EventCardSkeleton from '@/features/events/shared/EventCard.skeleton'

const ViewMoreEventsSkeleton: React.FC = () => {
  return (
    <main className="container mx-auto px-4 py-8">
      <nav className="flex mb-4" aria-label="Breadcrumb">
        <ol className="inline-flex items-center space-x-1 md:space-x-3">
          <li className="inline-flex items-center">
            <Link to="/" className="text-gray-700 hover:text-blue-600">
              Home
            </Link>
          </li>
          <li>
            <div className="flex items-center">
              <span className="mx-2 text-gray-400">/</span>
              <Skeleton className="h-4 w-20" />
            </div>
          </li>
        </ol>
      </nav>

      <Skeleton className="h-9 w-40 mb-6" />

      <div className="flex flex-col md:flex-row gap-8">
        <aside className="w-full md:w-1/4">
          <Skeleton className="h-[400px] w-full" />
        </aside>

        <div className="w-full md:w-3/4">
          <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
            {Array.from({ length: 6 }).map((_, index) => (
              <EventCardSkeleton key={index} />
            ))}
          </div>

          <div className="mt-8 flex justify-center">
            <Skeleton className="h-10 w-96" />
          </div>
        </div>
      </div>
    </main>
  )
}

export default ViewMoreEventsSkeleton