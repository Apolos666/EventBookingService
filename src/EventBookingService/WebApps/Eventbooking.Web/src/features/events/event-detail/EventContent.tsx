import React from 'react';
import { CalendarIcon, MapPinIcon } from 'lucide-react';
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar";
import { Event } from "../../../features/events/shared/event.types";
import { formatDate } from '@/utils/dateUtils';

interface EventContentProps {
    event: Event;
}

const EventContent: React.FC<EventContentProps> = ({ event }) => {
    return (
        <div className="w-full lg:w-2/3">
            <h1 className="text-3xl font-bold mb-4">{event.name}</h1>
            <div className="mb-6">
                <img
                    src={event.eventImageUrl}
                    alt="Summer Music Festival 2023"
                    width={800}
                    height={400}
                    className="rounded-lg"
                />
            </div>
            <p className="text-gray-600 mb-4">
                {event.description}
            </p>
            <div className="grid grid-cols-2 gap-4 mb-6">
                <div>
                    <h2 className="text-lg font-semibold mb-2">Start Date</h2>
                    <p className="flex items-center text-gray-600">
                        <CalendarIcon className="mr-2 h-5 w-5" />
                        {formatDate(new Date(event.startDateTime))}
                    </p>
                </div>
                <div>
                    <h2 className="text-lg font-semibold mb-2">End Date</h2>
                    <p className="flex items-center text-gray-600">
                        <CalendarIcon className="mr-2 h-5 w-5" />
                        {formatDate(new Date(event.endDateTime))}
                    </p>
                </div>
            </div>
            <div className="mb-6">
                <h2 className="text-lg font-semibold mb-2">Location</h2>
                <Tabs defaultValue={"0"}>
                    <TabsList>
                        {event.eventLocations?.map((location, index) => (
                            <TabsTrigger key={index} value={index.toString()}>
                                {location.location.name}
                            </TabsTrigger>
                        ))}
                    </TabsList>
                    {event.eventLocations?.map((location, index) => (
                        <TabsContent key={index} value={index.toString()}>
                            <p className="flex items-center text-gray-600">
                                <MapPinIcon className="mr-2 h-5 w-5" />
                                {`${location.location.address}, ${location.location.city}, ${location.location.state} ${location.location.zipCode}, ${location.location.country}`}
                            </p>
                            <p className="mt-2">
                                Price: ${location.price.toFixed(2)} |
                                Capacity: {location.registeredAttendees}/{location.maxAttendees}
                            </p>
                        </TabsContent>
                    ))}
                </Tabs>
            </div>
            <div className="mb-6">
                <h2 className="text-lg font-semibold mb-2">Refund Policy</h2>
                <p className="text-gray-600">
                    {event.refundPolicy}
                </p>
            </div>
            <div className="mb-6">
                <h2 className="text-lg font-semibold mb-2">About this event</h2>
                <p className="text-gray-600">
                    {event.aboutThisEvent}
                </p>
            </div>
            <div>
                <h2 className="text-lg font-semibold mb-2">Organized by</h2>
                <div className="flex items-center">
                    <Avatar className="h-12 w-12 mr-4">
                        <AvatarImage src="/placeholder.svg?text=MC" alt="Music City Events" />
                        <AvatarFallback>MC</AvatarFallback>
                    </Avatar>
                    <div>
                        <p className="font-semibold">Music City Events</p>
                        <p className="text-sm text-gray-600">Organizer of 50+ successful music festivals</p>
                    </div>
                </div>
            </div>
        </div>
    );
};

export default EventContent;