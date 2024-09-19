import React, { useState, useEffect } from 'react';
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { SearchIcon } from "lucide-react";
import EventCard from '../features/events/components/EventCard';
import { Event } from '../types';
import { Link } from 'react-router-dom';


const Homepage: React.FC = () => {
  const [events, setEvents] = useState<Event[]>([]);

  useEffect(() => {
    // Mock data fetch
    const mockEvents: Event[] = [
      {
        id: 1,
        title: "Summer Music Festival",
        date: "August 15, 2023",
        location: "Central Park",
        description: "A day of live music performances featuring top artists."
      },
      {
        id: 2,
        title: "Tech Conference 2023",
        date: "September 22, 2023",
        location: "Convention Center",
        description: "Annual gathering of tech innovators and industry leaders."
      },
      // Add more mock events as needed
    ];
    setEvents(mockEvents);
  }, []);

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
          {events.map((event) => (
            <EventCard key={event.id} {...event} />
          ))}
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