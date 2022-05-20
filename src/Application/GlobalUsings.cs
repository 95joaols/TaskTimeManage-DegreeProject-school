// Global using directives

global using Application.Common.Exceptions;
global using Application.Common.Interfaces;
global using Application.Common.Settings;
global using Application.CQRS.Authentication.Queries;
global using Application.CQRS.WorkItems.Commands;
global using Application.CQRS.WorkItems.Queries;
global using Application.CQRS.WorkTimes.Commands;
global using Ardalis.GuardClauses;
global using Domain.Aggregates.UserAggregate;
global using Domain.Aggregates.WorkAggregate;
global using MediatR;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;