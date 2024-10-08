﻿global using EventBooking.Event.Exceptions;
global using InvalidOperationException = BuildingBlocks.Exceptions.InvalidOperationException;
global using Carter;
global using Marten;
global using Weasel.Core;
global using BuildingBlocks.CQRS;
global using Mapster;
global using EventBooking.Event.Models;
global using MediatR;
global using BuildingBlocks.Behaviors;
global using FluentValidation;
global using BuildingBlocks.Pagination;
global using Marten.Pagination;
global using EventBooking.Event.Features;
global using EventBooking.Event.Extensions;
global using Microsoft.Extensions.Caching.Distributed;
global using EventBooking.Event.Data;
global using System.Text.Json;
global using Keycloak.AuthServices.Authentication;
global using Keycloak.AuthServices.Authorization;
global using Microsoft.AspNetCore.Authorization;
global using System.Security.Principal;
global using BuildingBlocks.Exceptions;
global using BuildingBlocks.Services.HttpAccessor;
global using MassTransit;
global using BuildingBlocks.Messaging.Events.Notifications; 
global using System.Reflection; 
global using BuildingBlocks.Messaging.MassTransit;
global using BuildingBlocks.Messaging.Events.Bookings;
global using EventBooking.Event.Features.EventStartDateChecker;
global using System.Security.Claims;
global using Microsoft.IdentityModel.Tokens;
global using BuildingBlocks.Enums;
global using Duende.AccessTokenManagement;
global using EventBooking.Storage.Protos; 
global using System.Text.Json.Serialization;



