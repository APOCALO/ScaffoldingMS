﻿using ErrorOr;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Common
{
    public abstract class ApiBaseHandler<TRequest, TResponse> : IRequestHandler<TRequest, ErrorOr<ApiResponse<TResponse>>>
        where TRequest : IRequest<ErrorOr<ApiResponse<TResponse>>>
    {
        protected readonly ILogger<ApiBaseHandler<TRequest, TResponse>> _logger;

        protected ApiBaseHandler(ILogger<ApiBaseHandler<TRequest, TResponse>> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ErrorOr<ApiResponse<TResponse>>> Handle(TRequest request, CancellationToken cancellationToken)
        {
            var stopwatch = Stopwatch.StartNew();

            // Log inicio de la gestión de solicitudes
            _logger.LogInformation("Handling {RequestName} with request: {@Request}", typeof(TRequest).Name, request);

            ErrorOr<ApiResponse<TResponse>> response;
            try
            {
                // Llamar a la implementación específica de la clase derivada
                response = await HandleRequest(request, cancellationToken);

                // Asignar el tiempo de respuesta
                response.Value.ResponseTime = stopwatch.Elapsed.TotalMilliseconds;

                // Log de finalización exitosa de la solicitud
                _logger.LogInformation("{RequestName} processed successfully with response: {@Response}", typeof(TRequest).Name, response.Value);
            }
            catch (Exception ex)
            {
                // Log los errores que puedan producirse
                _logger.LogError(ex, "An error occurred while handling {RequestName}", typeof(TRequest).Name);

                // Retornar un error genérico
                return Error.Failure($"An unexpected error occurred: {ex.Message}");
            }
            finally
            {
                // Parar el cronómetro y registrar el tiempo transcurrido
                stopwatch.Stop();
                _logger.LogInformation("{RequestName} completed in {ElapsedTime} ms.", typeof(TRequest).Name, stopwatch.Elapsed.TotalMilliseconds);
            }

            return response;
        }

        // Método abstracto que debe implementar la clase derivada
        protected abstract Task<ErrorOr<ApiResponse<TResponse>>> HandleRequest(TRequest request, CancellationToken cancellationToken);
    }
}
