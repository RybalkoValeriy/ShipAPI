using Application.Abstractions.Queries;

namespace Application.Ships.Get;

public record GetShipByIdQuery(string Code) : BaseQuery;