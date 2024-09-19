import React from 'react';
import EventHeader from '@/features/events/components/EventHeader';
import EventContent from '@/features/events/components/EventContent';
import EventSidebar from '@/features/events/components/EventSidebar';
import RelatedEvents from '@/features/events/components/RelatedEvents';

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