import { useEvents } from '../shared/queries/useEvents';
import EventCardSkeleton from '../shared/EventCard.skeleton';
import EventCard from '../shared/EventCard';

const RelatedEvents: React.FC = () => {
    const { data, isPending } = useEvents({ pageNumber: 1, pageSize: 3 });

    return (
        <section className="mt-12">
            <h2 className="text-2xl font-semibold mb-6">Other events you may like</h2>
            <div className="grid gap-6 md:grid-cols-2 lg:grid-cols-3">
                {isPending ? (
                    Array.from({ length: 3 }).map((_, index) => (
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
        </section>
    );
};

export default RelatedEvents;