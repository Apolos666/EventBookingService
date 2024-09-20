import { Notification } from './notification.types';

export const mockNotifications: Notification[] = [
    {
        id: '1',
        title: 'New Event Added',
        message: 'A new event "Summer Beach Party" has been added.',
        isRead: false,
        createdAt: '2023-06-10T09:00:00',
    },
    {
        id: '2',
        title: 'Ticket Sale Starting Soon',
        message: 'Ticket sales for "Tech Conference 2023" will start in 1 hour.',
        isRead: false,
        createdAt: '2023-06-11T14:30:00',
    },
    {
        id: '3',
        title: 'Event Reminder',
        message: 'Your event "Local Art Exhibition" starts tomorrow.',
        isRead: true,
        createdAt: '2023-06-12T10:15:00',
    },
];
