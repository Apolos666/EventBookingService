global using System.Reflection;
global using BuildingBlocks.Behaviors;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using BookingModel = Booking.Domain.Models;
global using Microsoft.EntityFrameworkCore;
global using BuildingBlocks.Exceptions;
global using Booking.Application.Dtos;
global using BuildingBlocks.CQRS;
global using Booking.Application.Data;
global using FluentValidation;
global using Booking.Application.Exceptions;
global using BuildingBlocks.Pagination;
global using Booking.Application.Extensions;
global using Booking.Domain.Events;
global using MediatR;
global using BuildingBlocks.Messaging.MassTransit; 
global using BuildingBlocks.Messaging.Events; 
global using MassTransit;
global using Booking.Application.Booking.Commands.CreateBooking;
global using Booking.Domain.ValueObjects;
global using DomainEventId = Booking.Domain.ValueObjects.EventId;
global using Microsoft.Extensions.Logging;
global using BuildingBlocks.Messaging.Events.Payments;
global using System.Data;
global using Microsoft.EntityFrameworkCore.Infrastructure;


