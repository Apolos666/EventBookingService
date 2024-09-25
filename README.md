# EventBooking: A Microservices-based Event Management System

## Introduction

<p align="justify">
⭐ This is a project demonstrating microservices-based event management system. The backend is built with .NET Core, applying Domain-Driven Design (DDD), Vertical Slice Architecture, and Clean Architecture principles. The frontend is developed using React with a focus on modern web technologies and best practices.
</p>

## The Goals of the Project

- [x] Building a cloud-native application with .NET Core
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
- [x] Notifications using `SignalR`
- [x] `Docker` and `Docker Compose` for containerization
- [] Testing

## Frontend Stack

- [x] Frontend built using `React` and `TypeScript`
- [x] Applying Vertical Slice Architecture on the frontend as well
- [x] State management and server-state synchronization with `React Query`
- [x] Styling with `Tailwind CSS` and `Shadcn`
- [x] `OIDC Client` with `PKCE` flow for secure authentication
- [x] Payment processing with `Stripe.js`
- [x] Real-time notifications with `SignalR.js`

- [eShop](https://github.com/dotnet/eShop)
- [practical-dotnet-aspire](https://github.com/thangchung/practical-dotnet-aspire)

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
