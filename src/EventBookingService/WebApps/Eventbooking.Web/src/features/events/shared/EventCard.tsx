import React from 'react';
import { Button } from "@/components/ui/button";
import { CalendarIcon, MapPinIcon } from "lucide-react";
import { Link } from 'react-router-dom';
import { formatDate } from '@/utils/dateUtils';

interface EventCardProps {
  id: string;
  name: string;
  startDate: Date;
  endDate: Date;
  location: string;
  description: string;
  imageUrl: string;
}

const EventCard: React.FC<EventCardProps> = ({ id, name, startDate, endDate, location, description, imageUrl }) => {
  return (
    <Link to={`/events/${id}`} className="border rounded-lg overflow-hidden hover:shadow-md transition-shadow">
      <div className="relative h-48">
        <img
          src={imageUrl}
          alt={name}
          className="object-cover w-full h-full"
        />
      </div>
      <div className="p-4">
        <div className="text-lg font-semibold mb-2">{name}</div>
        <div className="text-gray-600 mb-2 flex items-center">
          <CalendarIcon className="h-4 w-4 mr-2" />
          <span className="mr-1 font-semibold">Start:</span> {formatDate(startDate)}
        </div>
        <div className="text-gray-600 mb-2 flex items-center">
          <CalendarIcon className="h-4 w-4 mr-2" />
          <span className="mr-1 font-semibold">End:</span> {formatDate(endDate)}
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