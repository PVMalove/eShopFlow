using MediatR;

namespace Common.Kernel.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>, IBaseQuery
    where TResponse : notnull;

public interface IBaseQuery;