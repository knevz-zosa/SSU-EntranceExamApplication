using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Wrapper;
using MediatR;

namespace ApplicationLayer.Features.BaseCQS;

//Create
public abstract class BaseCreateCommand<TRequest> : IRequest<ResponseWrapper<int>>
{
    public TRequest Request { get; set; }
}

public abstract class BaseCreateCommandHandler<TCommand, TRequest> : IRequestHandler<TCommand, ResponseWrapper<int>>
    where TCommand : BaseCreateCommand<TRequest>
{
    protected readonly IUnitOfWork<int> _unitOfWork;
    public BaseCreateCommandHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public abstract Task<ResponseWrapper<int>> Handle(TCommand command, CancellationToken cancellationToken);
}

//Delete
public abstract class BaseDeleteCommand : IRequest<ResponseWrapper<int>>
{
    public int Id { get; set; }
}

public abstract class BaseDeleteCommandHandler<TCommand> : IRequestHandler<TCommand, ResponseWrapper<int>>
    where TCommand : BaseDeleteCommand
{
    protected readonly IUnitOfWork<int> _unitOfWork;

    public BaseDeleteCommandHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public abstract Task<ResponseWrapper<int>> Handle(TCommand command, CancellationToken cancellationToken);
}

//Update
public abstract class BaseUpdateCommand<TUpdate> : IRequest<ResponseWrapper<int>>
{
    public TUpdate Update { get; set; }
}

public abstract class BaseUpdateCommandHandler<TCommand, TUpdate> : IRequestHandler<TCommand, ResponseWrapper<int>>
    where TCommand : BaseUpdateCommand<TUpdate>
{
    protected readonly IUnitOfWork<int> _unitOfWork;
    public BaseUpdateCommandHandler(IUnitOfWork<int> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public abstract Task<ResponseWrapper<int>> Handle(TCommand command, CancellationToken cancellationToken);
}

//Get
public abstract class BaseGetQuery<TResponse> : IRequest<ResponseWrapper<TResponse>>
{
    public int Id { get; set; }

    protected BaseGetQuery(int id)
    {
        Id = id;
    }
}

public abstract class BaseGetQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, ResponseWrapper<TResponse>>
    where TQuery : BaseGetQuery<TResponse>
{
    protected readonly IUnitOfWork<int> _unitOfWork;
    protected readonly IMapper _mapper;

    public BaseGetQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public abstract Task<ResponseWrapper<TResponse>> Handle(TQuery get, CancellationToken cancellationToken);
}

//List
public abstract class BaseListQuery<TResponse> : IRequest<ResponseWrapper<PagedList<TResponse>>>
    where TResponse : class
{
    public DataGridQuery GridQuery { get; set; }
}

public abstract class BaseListQueryHandler<TListQuery, TResponse>
        : IRequestHandler<TListQuery, ResponseWrapper<PagedList<TResponse>>>
        where TListQuery : BaseListQuery<TResponse>
        where TResponse : class
{
    protected readonly IUnitOfWork<int> _unitOfWork;
    protected readonly IMapper _mapper;

    protected BaseListQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public abstract Task<ResponseWrapper<PagedList<TResponse>>> Handle(TListQuery list, CancellationToken cancellationToken);
}