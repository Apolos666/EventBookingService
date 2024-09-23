import EventContentSkeleton from "@/features/events/event-detail/EventContent.skeleton"
import EventHeader from "@/features/events/event-detail/EventHeader"
import EventSidebarSkeleton from "@/features/events/event-detail/EventSideBar.skeleton"
import RelatedEventsSkeleton from "@/features/events/event-detail/RelatedEvents.skeleton"

const EventDetailSkeleton: React.FC = () => {
    return (
      <main className="flex-grow container mx-auto px-4 py-8">
        <EventHeader />
        <div className="flex flex-col lg:flex-row gap-8">
          <EventContentSkeleton />
          <EventSidebarSkeleton />
        </div>
        <RelatedEventsSkeleton />
      </main>
    )
  }
  
  export default EventDetailSkeleton