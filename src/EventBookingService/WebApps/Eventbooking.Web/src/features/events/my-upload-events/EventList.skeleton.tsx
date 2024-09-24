import React from 'react';
import { Card, CardContent, CardFooter, CardHeader } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { Skeleton } from "@/components/ui/skeleton";
import { ChevronLeft, ChevronRight } from 'lucide-react';

const EventListSkeleton: React.FC = () => {
    return (
        <div className="container mx-auto py-8 px-4 sm:px-6 lg:px-8">
            <h1 className="text-3xl font-bold mb-6">My Booked Events</h1>
            <div className="relative">
                <Button
                    variant="outline"
                    size="icon"
                    className="absolute left-0 top-1/2 -translate-y-1/2 z-10 hidden sm:flex"
                    disabled
                    aria-label="Scroll left"
                >
                    <ChevronLeft className="h-4 w-4" />
                </Button>
                <Button
                    variant="outline"
                    size="icon"
                    className="absolute right-0 top-1/2 -translate-y-1/2 z-10 hidden sm:flex"
                    disabled
                    aria-label="Scroll right"
                >
                    <ChevronRight className="h-4 w-4" />
                </Button>
                <div className="flex overflow-x-auto sm:overflow-x-hidden space-x-4 sm:space-x-6 py-4 px-0 sm:px-8 snap-x snap-mandatory">
                    {[...Array(3)].map((_, index) => (
                        <Card key={index} className="flex flex-col w-[calc(100%-1rem)] sm:w-[calc(50%-1rem)] lg:w-[calc(33.333%-1rem)] flex-shrink-0 snap-center">
                            <CardHeader>
                                <Skeleton className="h-6 w-3/4 mb-2" />
                                <Skeleton className="h-4 w-full" />
                            </CardHeader>
                            <CardContent className="flex-grow">
                                <Skeleton className="w-full h-40 sm:h-48 mb-4" />
                                <Skeleton className="h-4 w-3/4 mb-2" />
                                <Skeleton className="h-4 w-1/2 mb-4" />
                                <div className="space-y-2">
                                    <Skeleton className="h-8 w-full" />
                                    <Skeleton className="h-8 w-full" />
                                    <Skeleton className="h-8 w-full" />
                                </div>
                            </CardContent>
                            <CardFooter className="flex flex-col sm:flex-row justify-between gap-2">
                                <Skeleton className="h-10 w-full sm:w-24" />
                                <Skeleton className="h-10 w-full sm:w-24" />
                            </CardFooter>
                        </Card>
                    ))}
                </div>
                <div className="flex justify-center mt-4 space-x-2">
                    {[...Array(3)].map((_, index) => (
                        <div
                            key={index}
                            className={`w-2 h-2 rounded-full ${index === 0 ? 'bg-primary' : 'bg-gray-300'}`}
                            aria-hidden="true"
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default EventListSkeleton;