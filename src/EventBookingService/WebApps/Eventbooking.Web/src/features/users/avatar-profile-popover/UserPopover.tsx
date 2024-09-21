import React, { useState } from 'react';
import { Avatar, AvatarImage, AvatarFallback } from "@/components/ui/avatar";
import { Button } from "@/components/ui/button";
import { Popover, PopoverContent, PopoverTrigger } from "@/components/ui/popover";
import { UserIcon, SettingsIcon, LogOutIcon } from "lucide-react";
import { IdTokenClaims } from 'oidc-client-ts';

interface UserPopoverProps {
  profile: IdTokenClaims;
  onSignOut: () => void;
}

const UserPopover: React.FC<UserPopoverProps> = ({ profile, onSignOut }) => {
  const [isPopoverOpen, setIsPopoverOpen] = useState(false);

  return (
    <Popover open={isPopoverOpen} onOpenChange={setIsPopoverOpen}>
      <PopoverTrigger asChild>
        <Avatar className="cursor-pointer">
          <AvatarImage src="https://github.com/shadcn.png" alt="@shadcn" />
          <AvatarFallback>{profile.name?.split(' ').map(n => n[0]).join('')}</AvatarFallback>
        </Avatar>
      </PopoverTrigger>
      <PopoverContent className="w-56">
        <div className="grid gap-4">
          <div className="font-medium">{profile.name}</div>
          <div className="grid grid-cols-2 gap-2">
            <Button variant="outline" size="sm" className="w-full justify-start">
              <UserIcon className="mr-2 h-4 w-4" />
              Profile
            </Button>
            <Button variant="outline" size="sm" className="w-full justify-start">
              <SettingsIcon className="mr-2 h-4 w-4" />
              Settings
            </Button>
          </div>
          <Button 
            variant="outline" 
            size="sm" 
            className="w-full justify-start text-red-500 hover:text-red-500 hover:bg-red-50"
            onClick={onSignOut}
          >
            <LogOutIcon className="mr-2 h-4 w-4" />
            Log out
          </Button>
        </div>
      </PopoverContent>
    </Popover>
  );
};

export default UserPopover;