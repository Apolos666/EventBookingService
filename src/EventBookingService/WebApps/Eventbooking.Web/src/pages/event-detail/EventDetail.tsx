import EventContent from '@/features/events/event-detail/EventContent';
import EventContentSkeleton from '@/features/events/event-detail/EventContent.skeleton';
import EventHeader from '@/features/events/event-detail/EventHeader';
import EventSidebar from '@/features/events/event-detail/EventSidebar';
import EventSidebarSkeleton from '@/features/events/event-detail/EventSideBar.skeleton';
import RelatedEvents from '@/features/events/event-detail/RelatedEvents';
import { useGetEventById } from '@/features/events/event-detail/useGetEventById';
import React from 'react';
import { useParams } from 'react-router-dom';

const EventDetail: React.FC = () => {
  const { id } = useParams<string>();

  const { data, isPending } = useGetEventById(id!);

  return (
    <main className="flex-grow container mx-auto px-4 py-8">
      <EventHeader />
      <div className="flex flex-col lg:flex-row gap-8">
        {isPending ? (
          <>
            <EventContentSkeleton />
            <EventSidebarSkeleton />
          </>
        ) : (
          data && (
            <>
              <EventContent event={data.event} />
              <EventSidebar event={data.event}/>
            </>
          )
        )}
      </div>
      <RelatedEvents />
    </main>
  );
};

export default EventDetail;