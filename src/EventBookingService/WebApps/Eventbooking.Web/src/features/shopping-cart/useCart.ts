import { useState } from 'react';
import { CartItem } from './types';

export const useCart = () => {
  const [cartItems, setCartItems] = useState<CartItem[]>([
    {
      id: '1',
      eventName: 'Summer Music Festival',
      startTime: '2023-07-15T14:00',
      quantity: 2,
      price: 150,
      creationTime: '2023-06-01T10:30',
    },
    {
      id: '2',
      eventName: 'Tech Conference 2023',
      startTime: '2023-08-22T09:00',
      quantity: 1,
      price: 299,
      creationTime: '2023-06-02T15:45',
    },
  ]);

  const addToCart = (item: CartItem) => {
    setCartItems([...cartItems, item]);
  };

  const removeFromCart = (id: string) => {
    setCartItems(cartItems.filter(item => item.id !== id));
  };

  const updateQuantity = (id: string, quantity: number) => {
    setCartItems(cartItems.map(item => 
      item.id === id ? { ...item, quantity } : item
    ));
  };

  const getTotalPrice = () => {
    return cartItems.reduce((total, item) => total + item.price * item.quantity, 0);
  };

  return {
    cartItems,
    addToCart,
    removeFromCart,
    updateQuantity,
    getTotalPrice,
  };
};