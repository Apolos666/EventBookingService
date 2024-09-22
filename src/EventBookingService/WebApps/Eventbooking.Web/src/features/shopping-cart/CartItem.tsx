import React from 'react';
import { formatDate } from '@/utils/dateUtils';
import { EventCartItem } from './shopping-cart.types';
import { Tooltip, TooltipContent, TooltipProvider, TooltipTrigger } from '@/components/ui/tooltip';
import { truncateText } from '@/utils/textUtils';

interface CartItemProps {
  item: EventCartItem;
}

const CartItem: React.FC<CartItemProps> = ({ item }) => {
  return (
    <div className="flex flex-col items-start p-2 w-full">
      <div className="flex justify-between w-full">
        <TooltipProvider>
          <Tooltip>
            <TooltipTrigger asChild>
              <span className="font-medium">{truncateText(item.eventName, 30)}</span>
            </TooltipTrigger>
            <TooltipContent>
              <p>{item.eventName}</p>
            </TooltipContent>
          </Tooltip>
        </TooltipProvider>
        <span>${(item.price * item.quantity).toFixed(2)}</span>
      </div>
      <TooltipProvider>
        <Tooltip>
          <TooltipTrigger asChild>
            <div className="text-sm text-gray-500 truncate w-full">
              {truncateText(item.eventLocationName, 30)}
            </div>
          </TooltipTrigger>
          <TooltipContent>
            <p>{item.eventLocationName}</p>
          </TooltipContent>
        </Tooltip>
      </TooltipProvider>
      <div className="text-sm text-gray-500">
        Start: {formatDate(new Date(item.startDateTime))}
      </div>
      <div className="text-sm text-gray-500">
        Quantity: {item.quantity}
      </div>
      <div className="text-sm text-gray-500">
        Price: ${item.price.toFixed(2)}
      </div>
    </div>
  );
};

export default CartItem;