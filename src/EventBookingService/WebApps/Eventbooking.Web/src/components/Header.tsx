import React from 'react';
import { Link } from 'react-router-dom';
import { Button } from "@/components/ui/button";
import { MenuIcon, LogInIcon } from "lucide-react";
import UserPopover from '@/features/users/avatar-profile-popover/UserPopover';
import CartDropdown from '@/features/shopping-cart/CartDropdown';
import AddEventDialog from '@/features/events/add-event/UploadEventDialog';
import NotificationDropdown from '@/features/notification/NotificationDropdown';
import { useAuth } from 'react-oidc-context';

const Header: React.FC = () => {
  const auth = useAuth();

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
          {auth.isAuthenticated && (
            <>
              <AddEventDialog />
              <CartDropdown />
              <NotificationDropdown />
            </> 
          )}
          {auth.isAuthenticated && auth.user ? (
            <UserPopover profile={auth.user.profile} onSignOut={() => void auth.removeUser()} />
          ) : (
            <Button variant="outline" onClick={() => void auth.signinRedirect()}>
              <LogInIcon className="mr-2 h-4 w-4" />
              Sign In
            </Button>
          )}
          <Button variant="ghost" size="icon" className="md:hidden">
            <MenuIcon className="h-6 w-6" />
          </Button>
        </div>
      </div>
    </header>
  );
};

export default Header;