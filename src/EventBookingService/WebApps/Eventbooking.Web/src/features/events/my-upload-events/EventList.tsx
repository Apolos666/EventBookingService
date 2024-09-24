import React, { useRef, useState, useEffect } from 'react';
import {
    Card,
    CardContent,
    CardDescription,
    CardFooter,
    CardHeader,
    CardTitle,
} from "@/components/ui/card";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import {
    Accordion,
    AccordionContent,
    AccordionItem,
    AccordionTrigger,
} from "@/components/ui/accordion";
import { Progress } from "@/components/ui/progress";
import { formatDate } from '@/utils/dateUtils';
import { EventLocation } from '../shared/event.types';
import { ChevronLeft, ChevronRight } from 'lucide-react';
import { useGetEventsUser } from './useGetEventsUser';
import EventListSkeleton from './EventList.skeleton';
import { Link } from 'react-router-dom';
import { pathKeys } from '@/pages/config.route';

const EventList: React.FC = () => {
    const { data, isPending } = useGetEventsUser();
    const scrollContainerRef = useRef<HTMLDivElement>(null);
    const [currentIndex, setCurrentIndex] = useState(0);
    const [visibleCards, setVisibleCards] = useState(3);

    useEffect(() => {
        const handleResize = () => {
            if (window.innerWidth < 640) {
                setVisibleCards(1);
            } else if (window.innerWidth < 1024) {
                setVisibleCards(2);
            } else {
                setVisibleCards(3);
            }

            if (scrollContainerRef.current) {
                scrollContainerRef.current.scrollTo({
                    left: currentIndex * (scrollContainerRef.current.offsetWidth / visibleCards),
                    behavior: 'auto'
                });
            }
        };

        handleResize();
        window.addEventListener('resize', handleResize);
        return () => window.removeEventListener('resize', handleResize);
    }, [currentIndex, visibleCards]);

    if (isPending) {
        return <EventListSkeleton />;
    }

    const scroll = (direction: 'left' | 'right') => {
        if (scrollContainerRef.current) {
            const scrollAmount = scrollContainerRef.current.offsetWidth;
            const newIndex = direction === 'left'
                ? Math.max(0, currentIndex - visibleCards)
                : Math.min(data?.events.length! - visibleCards, currentIndex + visibleCards);

            scrollContainerRef.current.scrollTo({
                left: newIndex * (scrollAmount / visibleCards),
                behavior: 'smooth'
            });

            setCurrentIndex(newIndex);
        }
    };

    return (
        <div className="container mx-auto py-8 px-4 sm:px-6 lg:px-8">
            <h1 className="text-3xl font-bold mb-6">My Booked Events</h1>
            <div className="relative">
                <Button
                    variant="outline"
                    size="icon"
                    className="absolute left-0 top-1/2 -translate-y-1/2 z-10 hidden sm:flex"
                    onClick={() => scroll('left')}
                    aria-label="Scroll left"
                    disabled={currentIndex === 0}
                >
                    <ChevronLeft className="h-4 w-4" />
                </Button>
                <Button
                    variant="outline"
                    size="icon"
                    className="absolute right-0 top-1/2 -translate-y-1/2 z-10 hidden sm:flex"
                    onClick={() => scroll('right')}
                    aria-label="Scroll right"
                    disabled={currentIndex >= data?.events.length! - visibleCards}
                >
                    <ChevronRight className="h-4 w-4" />
                </Button>
                <div
                    ref={scrollContainerRef}
                    className="flex overflow-x-auto sm:overflow-x-hidden space-x-4 sm:space-x-6 py-4 px-0 sm:px-8 snap-x snap-mandatory"
                >
                    {data?.events.map((event) => (
                        <Card key={event.id} className="flex flex-col w-[calc(100%-1rem)] sm:w-[calc(50%-1rem)] lg:w-[calc(33.333%-1rem)] flex-shrink-0 snap-center">
                            <CardHeader>
                                <CardTitle className="text-lg sm:text-xl">{event.name}</CardTitle>
                                <CardDescription className="text-sm">{event.description}</CardDescription>
                            </CardHeader>
                            <CardContent className="flex-grow">
                                <img src={event.eventImageUrl} alt={event.name} className="w-full h-40 sm:h-48 object-cover mb-4 rounded" />
                                <p className="text-sm"><strong>Start:</strong> {formatDate(event.startDateTime)}</p>
                                <p className="text-sm"><strong>End:</strong> {formatDate(event.endDateTime)}</p>
                                <Accordion type="single" collapsible className="mt-4">
                                    <AccordionItem value="locations">
                                        <AccordionTrigger className="text-sm">Event Locations</AccordionTrigger>
                                        <AccordionContent>
                                            {event.eventLocations.map((loc: EventLocation) => (
                                                <div key={loc.id} className="mb-4 p-4 border rounded text-sm">
                                                    <p><strong>{loc.location.name}</strong></p>
                                                    <p>{loc.location.address}, {loc.location.city}, {loc.location.state} {loc.location.zipCode}, {loc.location.country}</p>
                                                    <p>Price: ${loc.price}</p>
                                                    <p>Capacity: {loc.registeredAttendees}/{loc.maxAttendees}</p>
                                                    <div className="mt-2">
                                                        <Progress value={(loc.registeredAttendees / loc.maxAttendees) * 100} className="w-full" />
                                                    </div>
                                                    <Badge className="mt-2" variant={loc.registeredAttendees === loc.maxAttendees ? "destructive" : "secondary"}>
                                                        {loc.registeredAttendees === loc.maxAttendees ? "Sold Out" : "Available"}
                                                    </Badge>
                                                </div>
                                            ))}
                                        </AccordionContent>
                                    </AccordionItem>
                                    <AccordionItem value="refund">
                                        <AccordionTrigger className="text-sm">Refund Policy</AccordionTrigger>
                                        <AccordionContent>
                                            <p className="text-sm">{event.refundPolicy}</p>
                                        </AccordionContent>
                                    </AccordionItem>
                                    <AccordionItem value="about">
                                        <AccordionTrigger className="text-sm">About This Event</AccordionTrigger>
                                        <AccordionContent>
                                            <p className="text-sm">{event.aboutThisEvent}</p>
                                        </AccordionContent>
                                    </AccordionItem>
                                </Accordion>
                            </CardContent>
                            <CardFooter className="flex flex-col sm:flex-row justify-between gap-2">
                                <Button variant="outline" className="w-full sm:w-auto">Edit Event</Button>
                                <Link to={pathKeys.event_details.get_detail_path(event.id)}>
                                    <Button className="w-full sm:w-auto">View Details</Button>
                                </Link>
                            </CardFooter>
                        </Card>
                    ))}
                </div>
                <div className="flex justify-center mt-4 space-x-2">
                    {Array.from({ length: Math.ceil(data?.events.length! / visibleCards) }).map((_, index) => (
                        <div
                            key={index}
                            className={`w-2 h-2 rounded-full ${index === Math.floor(currentIndex / visibleCards) ? 'bg-primary' : 'bg-gray-300'
                                }`}
                            aria-hidden="true"
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default EventList;