import React, { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { PlusIcon, MinusIcon } from "lucide-react";
import { Event } from '../shared/event.types';
import { useAddUserCart } from '@/features/shopping-cart/useAddUserCart';
import { EventCartDto, EventCartItemDto } from '@/services/apis/baskets/types';

interface EventSidebarProps {
  event: Event;
}

const EventSidebar: React.FC<EventSidebarProps> = ({ event }) => {
  const { mutate, isPending } = useAddUserCart();
  const [quantity, setQuantity] = useState(1);
  const [selectedLocationId, setSelectedLocationId] = useState<string | null>(null);

  const increaseQuantity = () => setQuantity(prev => prev + 1);
  const decreaseQuantity = () => setQuantity(prev => Math.max(1, prev - 1));

  const handleAddToCart = () => {
    if (!selectedLocationId) {
      alert("Please select a ticket type");
      return;
    }

    const selectedLocation = event.eventLocations.find(loc => loc.id === selectedLocationId);
    if (!selectedLocation) {
      alert("Selected location not found");
      return;
    }

    const cartItem: EventCartItemDto = {
      eventId: event.id,
      startDateTime: event.startDateTime,
      eventLocationId: selectedLocationId,
      eventLocationName: `${selectedLocation.location.address}, ${selectedLocation.location.city}, ${selectedLocation.location.country}`,
      eventName: event.name,
      quantity: quantity,
      price: selectedLocation.price
    };

    const cartDto: EventCartDto = {
      createdAt: new Date(),
      updatedAt: new Date(),
      items: [cartItem]
    };

    mutate(cartDto);
  };

  return (
    <div className="w-full lg:w-1/3">
      <div className="border rounded-lg p-6 sticky top-4">
        <h2 className="text-xl font-semibold mb-4">Pricing</h2>
        <Select onValueChange={(value) => setSelectedLocationId(value)}>
          <SelectTrigger>
            <SelectValue placeholder="Select ticket type" />
          </SelectTrigger>
          <SelectContent>
            {event.eventLocations.map((location) => (
              <SelectItem key={location.id} value={location.id}>
                {`${location.location.address}, ${location.location.city}, ${location.location.country}`} - ${location.price}
              </SelectItem>
            ))}
          </SelectContent>
        </Select>
        <div className="flex items-center justify-between mt-4 mb-6">
          <span className="text-lg font-semibold">Quantity</span>
          <div className="flex items-center">
            <Button variant="outline" size="icon" onClick={decreaseQuantity}>
              <MinusIcon className="h-4 w-4" />
            </Button>
            <span className="mx-4 text-lg">{quantity}</span>
            <Button variant="outline" size="icon" onClick={increaseQuantity}>
              <PlusIcon className="h-4 w-4" />
            </Button>
          </div>
        </div>
        <Button 
          className="w-full" 
          onClick={handleAddToCart} 
          disabled={isPending}
        >
          {isPending ? 'Adding to Cart...' : 'Add to Cart'}
        </Button>
      </div>
    </div>
  );
};

export default EventSidebar;