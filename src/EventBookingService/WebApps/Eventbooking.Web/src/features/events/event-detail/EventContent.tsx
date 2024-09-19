import React from 'react';
import { CalendarIcon, MapPinIcon } from 'lucide-react';
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar";

const EventContent: React.FC = () => {
    return (
        <div className="w-full lg:w-2/3">
            <h1 className="text-3xl font-bold mb-4">Summer Music Festival 2023</h1>
            <div className="mb-6">
                <img
                    src="/placeholder.svg?height=400&width=800&text=Event+Image"
                    alt="Summer Music Festival 2023"
                    width={800}
                    height={400}
                    className="rounded-lg"
                />
            </div>
            <p className="text-gray-600 mb-4">
                Join us for the biggest music festival of the summer, featuring top artists from around the world.
            </p>
            <div className="grid grid-cols-2 gap-4 mb-6">
                <div>
                    <h2 className="text-lg font-semibold mb-2">Start Date</h2>
                    <p className="flex items-center text-gray-600">
                        <CalendarIcon className="mr-2 h-5 w-5" />
                        July 15, 2023 at 12:00 PM
                    </p>
                </div>
                <div>
                    <h2 className="text-lg font-semibold mb-2">End Date</h2>
                    <p className="flex items-center text-gray-600">
                        <CalendarIcon className="mr-2 h-5 w-5" />
                        July 17, 2023 at 11:59 PM
                    </p>
                </div>
            </div>
            <div className="mb-6">
                <h2 className="text-lg font-semibold mb-2">Location</h2>
                <Tabs defaultValue="location1">
                    <TabsList>
                        <TabsTrigger value="location1">Main Stage</TabsTrigger>
                        <TabsTrigger value="location2">Second Stage</TabsTrigger>
                        <TabsTrigger value="location3">Acoustic Tent</TabsTrigger>
                    </TabsList>
                    <TabsContent value="location1">
                        <p className="flex items-center text-gray-600">
                            <MapPinIcon className="mr-2 h-5 w-5" />
                            Sunset Park, 123 Festival Road, Music City
                        </p>
                    </TabsContent>
                    <TabsContent value="location2">
                        <p className="flex items-center text-gray-600">
                            <MapPinIcon className="mr-2 h-5 w-5" />
                            Riverside Arena, 456 Melody Lane, Music City
                        </p>
                    </TabsContent>
                    <TabsContent value="location3">
                        <p className="flex items-center text-gray-600">
                            <MapPinIcon className="mr-2 h-5 w-5" />
                            Green Meadows, 789 Harmony Avenue, Music City
                        </p>
                    </TabsContent>
                </Tabs>
            </div>
            <div className="mb-6">
                <h2 className="text-lg font-semibold mb-2">Refund Policy</h2>
                <p className="text-gray-600">
                    Full refunds available up to 7 days before the event. 50% refund available between 7 days and 24 hours before the event. No refunds within 24 hours of the event.
                </p>
            </div>
            <div className="mb-6">
                <h2 className="text-lg font-semibold mb-2">About this event</h2>
                <p className="text-gray-600">
                    The Summer Music Festival 2023 is a three-day extravaganza featuring over 50 artists across multiple genres. From rock to pop, jazz to electronic, there's something for every music lover. Enjoy food from local vendors, interactive art installations, and a vibrant festival atmosphere. Don't miss out on the biggest musical event of the year!
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