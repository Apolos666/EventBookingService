import React from 'react';
import { CartItem as CartItemType } from './shopping-cart.types';
import { formatDate } from '@/utils/dateUtils';

interface CartItemProps {
  item: CartItemType;
}

const CartItem: React.FC<CartItemProps> = ({ item }) => {
  return (
    <div className="flex flex-col items-start p-2 w-full">
      <div className="flex justify-between w-full">
        <span className="font-medium">{item.eventName}</span>
        <span>${(item.price * item.quantity).toFixed(2)}</span>
      </div>
      <div className="text-sm text-gray-500">
        Start: {formatDate(item.startTime)}
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