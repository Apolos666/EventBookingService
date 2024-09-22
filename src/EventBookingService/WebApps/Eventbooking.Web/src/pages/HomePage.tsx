import React from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { SearchIcon } from "lucide-react";
import EventCard from '../features/events/shared/EventCard';
import { Link } from 'react-router-dom';
import { useEvents } from '@/features/events/shared/queries/useEvents';
import EventCardSkeleton from '@/features/events/shared/EventCard.skeleton';

const Homepage: React.FC = () => {
  const { data, isPending } = useEvents({ pageNumber: 1, pageSize: 3 });

  return (
    <main className="container mx-auto px-4 py-8">
      <section className="text-center mb-12">
        <h1 className="text-4xl font-bold mb-4">Discover and Book Amazing Events</h1>
        <p className="text-xl text-gray-600 mb-6">Find and attend events that match your interests</p>
        <div className="flex max-w-md mx-auto">
          <Input
            type="text"
            placeholder="Search events..."
            className="rounded-r-none"
          />
          <Button className="rounded-l-none">
            <SearchIcon className="h-4 w-4 mr-2" />
            Search
          </Button>
        </div>
      </section>

      <section>
        <h2 className="text-2xl font-semibold mb-4">Upcoming Events</h2>
        <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
          {isPending ? (
            Array.from({ length: 3}).map((_, index) => (
              <EventCardSkeleton key={index} />
            ))
          ) : (
            data?.events && data.events.map((event) => (
              <EventCard
                key={event.id}
                startDate={new Date(event.startDateTime)}
                endDate={new Date(event.endDateTime)}
                location={`${event.eventLocations[0].location.address}, ${event.eventLocations[0].location.city}, ${event.eventLocations[0].location.country}`}
                imageUrl={event.eventImageUrl}
                {...event}
              />
            ))
          )}
        </div>
        <div className="text-center mt-8">
          <Link to={"/events"}>
            <Button variant="outline" size="lg">
              View More Events
            </Button>
          </Link>
        </div>
      </section>
    </main>
  );
};

export default Homepage;