import { useState } from "react";
import { mockNotifications } from "./notifications.mockdata";
import { Notification } from "./notification.types";

export const useNotifications = () => {
    const [notifications, setNotifications] = useState<Notification[]>(mockNotifications);

    const markNotificationAsRead = (id: string) => {
        setNotifications(notifications.map(notification =>
            notification.id === id ? { ...notification, isRead: true } : notification
        ));
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