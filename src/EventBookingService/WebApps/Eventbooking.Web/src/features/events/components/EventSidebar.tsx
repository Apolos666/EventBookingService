import React, { useState } from 'react';
import { Button } from "@/components/ui/button";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { PlusIcon, MinusIcon } from "lucide-react";

const EventSidebar: React.FC = () => {
  const [quantity, setQuantity] = useState(1);

  const increaseQuantity = () => setQuantity(prev => prev + 1);
  const decreaseQuantity = () => setQuantity(prev => Math.max(1, prev - 1));

  return (
    <div className="w-full lg:w-1/3">
      <div className="border rounded-lg p-6 sticky top-4">
        <h2 className="text-xl font-semibold mb-4">Pricing</h2>
        <Select>
          <SelectTrigger>
            <SelectValue placeholder="Select ticket type" />
          </SelectTrigger>
          <SelectContent>
            <SelectItem value="general">General Admission - $150</SelectItem>
            <SelectItem value="vip">VIP Pass - $300</SelectItem>
            <SelectItem value="backstage">Backstage Pass - $500</SelectItem>
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
        <Button className="w-full">Add to Cart</Button>
      </div>
    </div>
  );
};

export default EventSidebar;