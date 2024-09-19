// src/pages/ViewMoreEvents.tsx
import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import EventCard from '../components/EventCard';
import Pagination from '../components/Pagination';
import EventFilters from '@/features/events/components/EventFilters';

const ViewMoreEvents: React.FC = () => {
  const [currentPage, setCurrentPage] = useState(1);
  const totalPages = 5; // This should be calculated based on the total number of events

  const events = [
    { id: 1, title: "Event Title 1", date: "June 15, 2023", location: "Event Location", description: "Lorem ipsum dolor sit amet, consectetur adipiscing elit." },
    { id: 2, title: "Event Title 2", date: "June 16, 2023", location: "Event Location", description: "Sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." },
    // ... add more events as needed
  ];

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
            {events.map((event) => (
              <EventCard key={event.id} {...event} />
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