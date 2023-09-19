using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NewAvalon.Abstractions.Exceptions;
using NewAvalon.Domain.Errors;
using NewAvalon.Domain.Exceptions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace NewAvalon.Abstractions.Behaviors
{
    public sealed class ExceptionHandlingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> _logger;

        public ExceptionHandlingBehavior(ILogger<ExceptionHandlingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            try
            {
                return await next();
            }
            catch (BadRequestException badRequestException)
            {
                _logger.LogError(badRequestException, badRequestException.Message);

                throw new ApiException(
                    badRequestException.Title,
                    badRequestException.Error.Code,
                    StatusCodes.Status400BadRequest,
                    badRequestException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                _logger.LogError(notFoundException, notFoundException.Message);

                throw new ApiException(
                    notFoundException.Title,
                    notFoundException.Error.Code,
                    StatusCodes.Status404NotFound,
                    notFoundException.Message);
            }
            catch (ConflictException conflictException)
            {
                _logger.LogError(conflictException, conflictException.Message);

                throw new ApiException(
                    conflictException.Title,
                    conflictException.Error.Code,
                    StatusCodes.Status409Conflict,
                    conflictException.Message);
            }
            catch (UnProcessableEntityException unProcessableEntityException)
            {
                _logger.LogError(unProcessableEntityException, unProcessableEntityException.Message);

                throw new ApiException(
                    unProcessableEntityException.Title,
                    unProcessableEntityException.Error.Code,
                    StatusCodes.Status422UnprocessableEntity,
                    unProcessableEntityException.Message);
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, exception.Message);

                throw new ApiException(
                    Error.ServerError.Description,
                    Error.ServerError.Code,
                    StatusCodes.Status500InternalServerError,
                    "Something went wrong.");
            }
        }
    }
}
