import React from 'react';
import { Button } from "@/components/ui/button";
import { CalendarIcon, MapPinIcon } from "lucide-react";

const RelatedEvents: React.FC = () => {
    return (
        <section className="mt-12">
            <h2 className="text-2xl font-semibold mb-6">Other events you may like</h2>
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
                {[1, 2, 3].map((event) => (
                    <div key={event} className="border rounded-lg overflow-hidden hover:shadow-md transition-shadow">
                        <div className="relative h-48">
                            <img
                                src={`/placeholder.svg?height=200&width=400&text=Event+Image+${event}`}
                                alt={`Event ${event}`}
                                className="object-cover w-full h-full"
                            />
                        </div>
                        <div className="p-4">
                            <div className="text-lg font-semibold mb-2">Similar Event {event}</div>
                            <div className="text-gray-600 mb-2 flex items-center">
                                <CalendarIcon className="h-4 w-4 mr-2" />
                                August {event + 14}, 2023
                            </div>
                            <div className="text-gray-600 mb-2 flex items-center">
                                <MapPinIcon className="h-4 w-4 mr-2" />
                                Event Location
                            </div>
                            <Button variant="outline" className="w-full">View Details</Button>
                        </div>
                    </div>
                ))}
            </div>
        </section>
    );
};

export default RelatedEvents;