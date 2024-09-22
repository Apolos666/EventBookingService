import React from 'react';
import { Button } from "@/components/ui/button";
import { ShoppingCartIcon } from "lucide-react";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import CartItem from './CartItem';
import { useUserCart } from './useUserCart';
import CartDropdownSkeleton from './CartDropdown.skeleton';

const CartDropdown: React.FC = () => {
  const { data, isPending } = useUserCart();

  if (isPending) {
    return <CartDropdownSkeleton />;
  }

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="outline" size="icon" className="relative">
          <ShoppingCartIcon className="h-4 w-4" />
          {data && data.cart.items?.length > 0 && (
            <span className="absolute -top-2 -right-2 bg-primary text-primary-foreground text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center">
              {data.cart.items.length}
            </span>
          )}
          <span className="sr-only">Shopping cart</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-80">
        <DropdownMenuLabel>Shopping Cart</DropdownMenuLabel>
        <DropdownMenuSeparator />
        {data?.cart.items.map((item) => (
          <DropdownMenuItem key={item.id} className="p-0">
            <CartItem item={item} />
          </DropdownMenuItem>
        ))}
        <DropdownMenuSeparator />
        <DropdownMenuItem className="flex justify-between">
          <span className="font-bold">Total</span>
          <span className="font-bold">${data?.cart.totalPrice.toFixed(2)}</span>
        </DropdownMenuItem>
        <DropdownMenuSeparator />
        <DropdownMenuItem>
          <Button className="w-full">Checkout</Button>
        </DropdownMenuItem>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default CartDropdown;