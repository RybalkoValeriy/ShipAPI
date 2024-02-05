using Application.Abstractions.Commands;

namespace Application.Ships.Delete;

public record DeleteShipCommand(string Code) : BaseCommand;
