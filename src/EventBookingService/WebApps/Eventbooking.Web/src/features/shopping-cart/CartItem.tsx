import React from 'react';
import { formatDate } from '@/utils/dateUtils';
import { EventCartItem } from '@/services/apis/baskets/types';

interface CartItemProps {
  item: EventCartItem;
}

const CartItem: React.FC<CartItemProps> = ({ item }) => {
  return (
    <div className="flex flex-col items-start p-2 w-full">
      <div className="flex justify-between w-full">
        <span className="font-medium">{item.eventName}</span>
        <span>${(item.price * item.quantity).toFixed(2)}</span>
      </div>
      <div className="text-sm text-gray-500">
        Start: {formatDate(new Date(item.startDateTime))}
      </div>
      <div className="text-sm text-gray-500">
        Quantity: {item.quantity}
      </div>
      <div className="text-sm text-gray-500">
        Price: {item.price}$
      </div>
    </div>
  );
};

export default CartItem;