import { useEffect, useRef, useState } from "react";
import { Notification } from "./notification.types";
import { useAuth } from "react-oidc-context";
import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { v4 as uuidv4 } from "uuid";
import { useToast } from "@/hooks/use-toast";

export const useNotifications = () => {
    const auth = useAuth();
    const connectionRef = useRef<HubConnection | null>(null);
    const [notifications, setNotifications] = useState<Notification[]>([]);
    const { toast } = useToast();

    useEffect(() => {
        const connection = new HubConnectionBuilder()
            .withUrl(import.meta.env.VITE_NOTIFICATIONS_API, { accessTokenFactory: () => auth.user?.access_token || "" })
            .build();

        connection.on("ReceiveNotification", (title, message) => {
            const newNotification: Notification = {
                id: uuidv4(),
                title,
                message,
                isRead: false,
                createdAt: new Date().toISOString(),
            }

            setNotifications(prevNotifications => [...prevNotifications, newNotification]);

            toast({
                title: newNotification.title,
                description: newNotification.message,
                duration: 5000, 
            });
        });  
    
        connection.start()
            .then(() => {
                console.log("Connected to notification hub");
            })
            .catch((err) => console.log("Error connecting to hub", err));

        connectionRef.current = connection;

        return () => {
            if (connectionRef.current) {
                connectionRef.current.stop();
            }
        };
    }, [auth.user?.access_token, toast]);

    const markNotificationAsRead = (id: string) => {
        setNotifications(prevNotifications => 
            prevNotifications.map(notification => 
                notification.id === id ? { ...notification, isRead: true } : notification
            )
        );
    };

    const getUnreadNotificationsCount = () => {
        return notifications.filter(notification => !notification.isRead).length;
    };

    return {
        notifications,
        markNotificationAsRead,
        getUnreadNotificationsCount,
    };
};