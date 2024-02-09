using Application.Abstractions.Commands;
using Application.Abstractions.Queries;
using Application.Ships;
using Application.Ships.Create;
using Application.Ships.Delete;
using Application.Ships.Get;
using Application.Ships.Update;
using Microsoft.AspNetCore.Mvc;

namespace ShipApi.Controllers;

[Route("api/v1/[controller]")]
public class ShipsController : Controller
{
    private readonly IQueryResolver _queryResolver;
    private readonly ICommandResolver _commandResolver;

    public ShipsController(
        IQueryResolver queryResolver,
        ICommandResolver commandResolver)
    {
        _queryResolver = queryResolver;
        _commandResolver = commandResolver;
    }

    /// <summary>
    /// Create a new Ship
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShipRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var command = new CreateShipCommand(request.Code, request.Name, request.Length, request.Width);

        await _commandResolver
            .ResolveFor<CreateShipCommand>()
            .SendAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Delete ship
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpDelete("{code}")]
    public async Task<IActionResult> Delete([FromRoute] string code, CancellationToken cancellationToken)
    {
        await _commandResolver
            .ResolveFor<DeleteShipCommand>()
            .SendAsync(new DeleteShipCommand(code), cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Update ship
    /// </summary>
    /// <param name="code"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPut("{code}")]
    public async Task<IActionResult> Update([FromRoute] string code, [FromBody] ShipRequest request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        var command = new UpdateShipCommand(code, request.Name, request.Length, request.Width);

        await _commandResolver
            .ResolveFor<UpdateShipCommand>()
            .SendAsync(command, cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Get all ships
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ShipResponse>>> GetAll(CancellationToken cancellationToken)
    {
        var ships = await _queryResolver
            .ResolveFor<GetAllShipsQuery, IEnumerable<ShipResponse>>()
            .SendAsync(new GetAllShipsQuery(), cancellationToken);

        return Ok(ships);
    }

    /// <summary>
    /// Get ship by id
    /// </summary>
    /// <param name="code"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("{code}")]
    public async Task<ActionResult<ShipResponse>> Get([FromRoute] string code, CancellationToken cancellationToken)
    {
        var ship = await _queryResolver
            .ResolveFor<GetShipByIdQuery, ShipResponse>()
            .SendAsync(new GetShipByIdQuery(code), cancellationToken);

        return Ok(ship);
    }
}
