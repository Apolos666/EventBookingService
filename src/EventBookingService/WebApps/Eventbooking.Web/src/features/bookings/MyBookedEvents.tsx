import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table"
import { Badge } from "@/components/ui/badge"
import { Accordion, AccordionContent, AccordionItem, AccordionTrigger } from "@/components/ui/accordion"
import MyBookedEventsSkeleton from "./MyBookedEvents.skeleton"
import { formatDate } from "@/utils/dateUtils"
import { useGetUserBookings } from "./useGetUserBookings"

const MyBookedEvents: React.FC = () => {
    const { data, isPending } = useGetUserBookings();

    if (isPending) {
        return <MyBookedEventsSkeleton />
    }

    return (
        <div className="container mx-auto py-8">
            <h1 className="text-3xl font-bold mb-6">My Booked Events</h1>
            <Table>
                <TableHeader>
                    <TableRow>
                        <TableHead>Booking ID</TableHead>
                        <TableHead>Status</TableHead>
                        <TableHead>Total Quantity</TableHead>
                        <TableHead>Total Price</TableHead>
                        <TableHead>Booked On</TableHead>
                        <TableHead>Details</TableHead>
                    </TableRow>
                </TableHeader>
                <TableBody>
                    {data?.bookings.map((booking) => (
                        <TableRow key={booking.id}>
                            <TableCell>{booking.id}</TableCell>
                            <TableCell>
                                <Badge variant={booking.bookingStatus === 'Confirmed' ? 'success' : 'secondary'}>
                                    {booking.bookingStatus}
                                </Badge>
                            </TableCell>
                            <TableCell>{booking.totalQuantity}</TableCell>
                            <TableCell>${booking.totalPrice.toFixed(2)}</TableCell>
                            <TableCell>{formatDate(new Date(booking.createdAt))}</TableCell>
                            <TableCell>
                                <Accordion type="single" collapsible>
                                    <AccordionItem value="items">
                                        <AccordionTrigger>View Items</AccordionTrigger>
                                        <AccordionContent>
                                            <Table>
                                                <TableHeader>
                                                    <TableRow>
                                                        <TableHead>Event</TableHead>
                                                        <TableHead>Date</TableHead>
                                                        <TableHead>Location</TableHead>
                                                        <TableHead>Quantity</TableHead>
                                                        <TableHead>Price</TableHead>
                                                        <TableHead>Total</TableHead>
                                                        <TableHead>Code</TableHead>
                                                    </TableRow>
                                                </TableHeader>
                                                <TableBody>
                                                    {booking.bookingItems.map((item, index) => (
                                                        <TableRow key={index}>
                                                            <TableCell>{item.eventName}</TableCell>
                                                            <TableCell>{formatDate(new Date(item.startDateTime))}</TableCell>
                                                            <TableCell>{item.eventLocationName}</TableCell>
                                                            <TableCell>{item.quantity}</TableCell>
                                                            <TableCell>${item.price.toFixed(2)}</TableCell>
                                                            <TableCell>${item.totalPrice.toFixed(2)}</TableCell>
                                                            <TableCell>{item.code}</TableCell>
                                                        </TableRow>
                                                    ))}
                                                </TableBody>
                                            </Table>
                                        </AccordionContent>
                                    </AccordionItem>
                                </Accordion>
                            </TableCell>
                        </TableRow>
                    ))}
                </TableBody>
            </Table>
        </div>
    )
}


export default MyBookedEvents