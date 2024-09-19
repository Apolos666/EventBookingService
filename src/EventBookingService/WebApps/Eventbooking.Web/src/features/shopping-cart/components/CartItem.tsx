import React from 'react';
import { CartItem as CartItemType } from '../types/cart';

interface CartItemProps {
  item: CartItemType;
}

const CartItem: React.FC<CartItemProps> = ({ item }) => {
  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit',
    });
  };

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
      <div className="text-xs text-gray-400">
        Added: {formatDate(item.creationTime)}
      </div>
    </div>
  );
};

export default CartItem;