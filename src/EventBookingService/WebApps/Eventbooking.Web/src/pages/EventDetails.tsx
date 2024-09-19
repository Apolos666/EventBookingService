import EventContent from '@/features/events/event-detail/EventContent';
import EventHeader from '@/features/events/event-detail/EventHeader';
import EventSidebar from '@/features/events/event-detail/EventSidebar';
import RelatedEvents from '@/features/events/event-detail/RelatedEvents';
import React from 'react';

const EventDetails: React.FC = () => {
  return (
    <main className="flex-grow container mx-auto px-4 py-8">
      <EventHeader />
      <div className="flex flex-col lg:flex-row gap-8">
        <EventContent />
        <EventSidebar />
      </div>
      <RelatedEvents />
    </main>
  );
};

export default EventDetails;