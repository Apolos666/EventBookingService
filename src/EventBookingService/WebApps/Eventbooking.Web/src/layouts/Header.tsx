import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from "@/components/ui/button";
import { MenuIcon } from "lucide-react";
import UserPopover from '@/features/users/avatar-profile-popover/UserPopover';
import AddEventDialog from '@/features/events/add-event/AddEventDialog';
import CartDropdown from '@/features/shopping-cart/CartDropdown';
import NotificationDropdown from '@/features/notification/NotificationDropDown';

const Header: React.FC = () => {
  return (
    <header className="border-b">
      <div className="container mx-auto px-4 py-4 flex items-center justify-between">
        <Link to="/" className="text-2xl font-bold">EventBook</Link>
        <div className="flex items-center space-x-4">
          <nav className="hidden md:flex space-x-4">
            <Link to="/" className="hover:underline">Home</Link>
            <Link to="/events" className="hover:underline">Events</Link>
            <Link to="/about" className="hover:underline">About</Link>
            <Link to="/contact" className="hover:underline">Contact</Link>
          </nav>
          <AddEventDialog />
          <CartDropdown />
          <NotificationDropdown />
          <UserPopover />
          <Button variant="ghost" size="icon" className="md:hidden">
            <MenuIcon className="h-6 w-6" />
          </Button>
        </div>
      </div>
    </header>
  );
};

export default Header;