using Application.Abstractions.Commands;

namespace Application.Ships.Update;

public record UpdateShipCommand(string Code, string Name, int Length, int Width) : BaseCommand;
