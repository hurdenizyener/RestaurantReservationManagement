using Application.Features.Reservations.Commands.Create;
using Application.Features.Reservations.Commands.Delete;
using Application.Features.Reservations.Commands.Update;
using Application.Features.Reservations.Commands.UpdateConfirmation;
using Application.Features.Reservations.Queries.GetAllList;
using Application.Features.Reservations.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.Extensions;

namespace WebAPI.Endpoints;

public class Reservations : BaseEndpointGroup
{
    public override void Map(WebApplication app)
    {
        var builder = app;
        var endpointBase = nameof(Reservations).ToLower();

        builder
            .MapEndpoint("POST", CreateReservation, $"/{endpointBase}")
            .MapEndpoint("DELETE", DeleteReservation, $"/{endpointBase}/{{id:guid}}")
            .MapEndpoint("PUT", UpdateReservation, $"/{endpointBase}")
            .MapEndpoint("PATCH", UpdateConfirmation, $"/{endpointBase}/{{id:guid}}")
            .MapEndpoint("GET", GetReservationById, $"/{endpointBase}/{{id:guid}}")
            .MapEndpoint("GET", GetAllReservation, $"/{endpointBase}/getalllist");
    }

    public async Task<IResult> CreateReservation(ISender sender, [FromBody] CreateReservationCommand command)
    {
        CreatedReservationResponse response = await sender.Send(command);
        return Results.Created(uri: $"/api/reservations/{response.Id}", value: response.Message);
    }

    public async Task<IResult> DeleteReservation(ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteReservationCommand(id));
        return Results.NoContent();
    }

    public async Task<IResult> UpdateReservation(ISender sender, [FromBody] UpdateReservationCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> UpdateConfirmation(ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new UpdateConfirmationCommand(id));
        return Results.NoContent();
    }

    public async Task<IResult> GetReservationById(ISender sender, [FromRoute] Guid id)
    {
        GetReservationByIdResponse response = await sender.Send(new GetReservationByIdQuery(id));
        return Results.Ok(response);
    }

    public async Task<IResult> GetAllReservation(ISender sender)
    {
        var query = new GetAllReservationsQuery();
        List<GetAllReservationsResponse> response = await sender.Send(query);

        return Results.Ok(response);
    }
}
