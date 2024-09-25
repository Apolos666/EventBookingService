# EventBooking: A Microservices-based Event Management System

## Introduction

<p align="justify">
⭐ This is a project demonstrating microservices-based event management system. The backend is built with .NET Core, applying Domain-Driven Design (DDD), Vertical Slice Architecture, and Clean Architecture principles. The frontend is developed using React with a focus on modern web technologies and best practices.
</p>

## The Goals of the Project

- [x] Building a cloud-native application with .NET Core
- [x] `Docker` and `Docker Compose` for containerization
- [x] Using `Vertical Slice Architecture` and `Clean Architecture` for organizing the codebase
- [x] Using `Domain-Driven Design` to design the domain model
- [x] Implementing the `CQRS` pattern with `MediatR` (including Pipelines for validation and logging)
- [x] Using `RabbitMQ` on top `MassTransit` for messaging
- [x] Identity and Access Management with Keycloak
- [x] Secure inter-service communication using `gRPC` with `Keycloak`
- [x] API Gateway using `YARP`
- [x] Using `Marten` with `PostgreSQL` as a document DB
- [x] Implementing `Entity Framework` and `Dapper` for separate read and write models
- [x] `Redis` for Caching (Output, Response, and Distributed Caching)
- [x] Using `Minimal API` with `Carter`
- [x] Input validation using `FluentValidation`
- [x] Payments integration with `Stripe`
- [x] Notifications using `SignalR` and `Background Service`.

## Frontend Stack

- [x] Frontend built using `React` and `TypeScript`
- [x] Applying Vertical Slice Architecture on the frontend as well
- [x] State management and server-state synchronization with `React Query`
- [x] Styling with `Tailwind CSS` and `Shadcn`
- [x] `OIDC Client` with `PKCE` flow for secure authentication
- [x] Payment processing with `Stripe.js`
- [x] Real-time notifications with `SignalR.js`

## Domain Business & Bounded Contexts - Services Boundaries

- **Basket**: Manages user shopping cart information.
- **Booking**: Handles user order information and processing.
- **Discount**: Provides discount information for events and applies discounts to orders.
- **Event**: Stores and manages event information, including creator, venue, start time, maximum attendees, etc.
- **Notification**: Sends notifications to users using a background service..
- **Payment**: Processes one-time payments using Stripe.
- **Storage**: Manages static files such as images and videos.

## Project References

- [eShopMicroservice](https://github.com/mehmetozkaya/EShopMicroservices)
- [practical-dotnet-aspire](https://github.com/foxminchan/BookWorm)
- [vertical-slice-react](https://github.com/yurisldk/realworld-react-fsd)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
