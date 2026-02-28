using _07._TaskFlow_CQRS.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _07._TaskFlow_CQRS.Application.Features.Auth.Commands;

public record RegisterCommand(RegisterRequest Request) : IRequest<AuthResponseDto>;
