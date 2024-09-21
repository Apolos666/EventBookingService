// src/pages/ViewMoreEvents.tsx
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import EventCard from '../features/events/shared/EventCard';
import Pagination from '../components/Pagination';
import EventFilters from '@/features/events/browsing-event/EventFilters';
import { useEvents } from '@/features/events/shared/queries/useEvents';

const ViewMoreEvents: React.FC = () => {
  const { data } = useEvents({ pageNumber: 1, pageSize: 6 });

  const [currentPage, setCurrentPage] = useState(1);
  const totalPages = 5;

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
              <span className="text-gray-500">All Events</span>
            </div>
          </li>
        </ol>
      </nav>

      <h1 className="text-3xl font-bold mb-6">All Events</h1>

      <div className="flex flex-col md:flex-row gap-8">
        <aside className="w-full md:w-1/4">
          <EventFilters />
        </aside>

        <div className="w-full md:w-3/4">
          <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
            {data?.events && data.events.map((event) => (
              <EventCard
                key={event.id}
                startDate={new Date(event.startDateTime)}
                endDate={new Date(event.endDateTime)}
                location={`${event.eventLocations[0].location.address}, ${event.eventLocations[0].location.city}, ${event.eventLocations[0].location.country}`}
                imageUrl={event.eventImageUrl}
                {...event}
              />
            ))}
          </div>

          <Pagination
            currentPage={currentPage}
            totalPages={totalPages}
            onPageChange={setCurrentPage}
          />
        </div>
      </div>
    </main>
  );
};

export default ViewMoreEvents;