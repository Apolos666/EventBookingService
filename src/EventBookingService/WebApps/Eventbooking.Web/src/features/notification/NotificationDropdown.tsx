import React from 'react';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuLabel,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";
import { Button } from "@/components/ui/button";
import { BellIcon } from "lucide-react";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import { ScrollArea } from "@/components/ui/scroll-area";
import { useNotifications } from './useNotifications';
import { formatDate } from '@/utils/dateUtils';

const NotificationDropdown: React.FC = () => {
  const { notifications, markNotificationAsRead, getUnreadNotificationsCount } = useNotifications();

  return (
    <DropdownMenu>
      <DropdownMenuTrigger asChild>
        <Button variant="outline" size="icon" className="relative">
          <BellIcon className="h-4 w-4" />
          {getUnreadNotificationsCount() > 0 && (
            <span className="absolute -top-2 -right-2 bg-primary text-primary-foreground text-xs font-bold rounded-full h-5 w-5 flex items-center justify-center">
              {getUnreadNotificationsCount()}
            </span>
          )}
          <span className="sr-only">Notifications</span>
        </Button>
      </DropdownMenuTrigger>
      <DropdownMenuContent className="w-80">
        <DropdownMenuLabel>Notifications</DropdownMenuLabel>
        <DropdownMenuSeparator />
        <Tabs defaultValue="unread" className="w-full">
          <TabsList className="grid w-full grid-cols-2">
            <TabsTrigger value="unread">Unread</TabsTrigger>
            <TabsTrigger value="read">Read</TabsTrigger>
          </TabsList>
          <TabsContent value="unread">
            <ScrollArea className="h-[300px]">
              {notifications.filter(notification => !notification.isRead).map((notification) => (
                <DropdownMenuItem key={notification.id} className="flex flex-col items-start cursor-pointer" onSelect={() => markNotificationAsRead(notification.id)}>
                  <div className="font-medium">{notification.title}</div>
                  <div className="text-sm text-gray-500">{notification.message}</div>
                  <div className="text-xs text-gray-400">{formatDate(notification.createdAt)}</div>
                </DropdownMenuItem>
              ))}
            </ScrollArea>
          </TabsContent>
          <TabsContent value="read">
            <ScrollArea className="h-[300px]">
              {notifications.filter(notification => notification.isRead).map((notification) => (
                <DropdownMenuItem key={notification.id} className="flex flex-col items-start">
                  <div className="font-medium">{notification.title}</div>
                  <div className="text-sm text-gray-500">{notification.message}</div>
                  <div className="text-xs text-gray-400">{formatDate(notification.createdAt)}</div>
                </DropdownMenuItem>
              ))}
            </ScrollArea>
          </TabsContent>
        </Tabs>
      </DropdownMenuContent>
    </DropdownMenu>
  );
};

export default NotificationDropdown;