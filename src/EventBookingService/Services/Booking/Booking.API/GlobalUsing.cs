global using Carter;
global using Booking.API;
global using Booking.Application;
global using Booking.Infrastructure;
global using Booking.Application.Dtos;
global using MediatR;
global using Booking.Application.Booking.Commands.CreateBooking;
global using Mapster;
global using BookingModel = Booking.Domain.Models;
global using Booking.Application.Booking.Commands.DeleteBooking;
global using BuildingBlocks.Pagination;
global using Booking.Application.Booking.Queries.GetBookings;
global using Booking.Application.Booking.Queries.GetBookingsByUserId;
global using BuildingBlocks.Services.HttpAccessor;
global using Microsoft.AspNetCore.Mvc;
global using Booking.Infrastructure.Data.Extensions;
global using System.Security.Claims;
global using Microsoft.IdentityModel.Tokens;


