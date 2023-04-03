using Domain.Shared;
using MediatR;

namespace Application.Behaviors
{
    public class ValidationPipeLineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
            where TRequest : IRequest
            where TResponse : Result
    { 
        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
