import React from 'react';
import { Button } from "@/components/ui/button";
import { CalendarIcon, MapPinIcon } from "lucide-react";
import { Link } from 'react-router-dom';

interface EventCardProps {
  id: number;
  title: string;
  date: string;
  location: string;
  description: string;
}

const EventCard: React.FC<EventCardProps> = ({ id, title, date, location, description }) => {
  return (
    <Link to={"/events/1"} className="border rounded-lg overflow-hidden hover:shadow-md transition-shadow">
      <div className="relative h-48">
        <img
          src={`/placeholder.svg?height=200&width=400&text=Event+Image+${id}`}
          alt={title}
          className="object-cover w-full h-full"
        />
      </div>
      <div className="p-4">
        <div className="text-lg font-semibold mb-2">{title}</div>
        <div className="text-gray-600 mb-2 flex items-center">
          <CalendarIcon className="h-4 w-4 mr-2" />
          {date}
        </div>
        <div className="text-gray-600 mb-2 flex items-center">
          <MapPinIcon className="h-4 w-4 mr-2" />
          {location}
        </div>
        <p className="text-sm text-gray-500 mb-4">{description}</p>
        <Button variant="outline" className="w-full">Book Now</Button>
      </div>
    </Link>
  );
};

export default EventCard;