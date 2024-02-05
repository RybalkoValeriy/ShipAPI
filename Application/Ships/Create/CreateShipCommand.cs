using Application.Abstractions.Commands;

namespace Application.Ships.Create;

public record CreateShipCommand(string Code, string Name, int Length, int Width) : BaseCommand;