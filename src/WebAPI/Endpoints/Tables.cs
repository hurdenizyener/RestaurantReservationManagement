using Application.Features.Tables.Commands.Create;
using Application.Features.Tables.Commands.Delete;
using Application.Features.Tables.Commands.Update;
using Application.Features.Tables.Queries.GetAllList;
using Application.Features.Tables.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.Extensions;

namespace WebAPI.Endpoints;

public class Tables : BaseEndpointGroup
{
    public override void Map(WebApplication app)
    {
        var builder = app;
        var endpointBase = nameof(Tables).ToLower();

        builder
            .MapEndpoint("POST", CreateTable, $"/{endpointBase}")
            .MapEndpoint("DELETE", DeleteTable, $"/{endpointBase}/{{id:guid}}")
            .MapEndpoint("PUT", UpdateTable, $"/{endpointBase}")
            .MapEndpoint("GET", GetTableById, $"/{endpointBase}/{{id:guid}}")
            .MapEndpoint("GET", GetAllTables, $"/{endpointBase}/getalltables");
    }

    public async Task<IResult> CreateTable(ISender sender, [FromBody] CreateTableCommand command)
    {
        CreatedTableResponse response = await sender.Send(command);
        return Results.Created(uri: $"/api/tables/{response.Id}", value: response);
    }

    public async Task<IResult> DeleteTable(ISender sender, [FromRoute] Guid id)
    {
        await sender.Send(new DeleteTableCommand(id));
        return Results.NoContent();
    }

    public async Task<IResult> UpdateTable(ISender sender, [FromBody] UpdateTableCommand command)
    {
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> GetTableById(ISender sender, [FromRoute] Guid id)
    {
        GetTableByIdResponse response = await sender.Send(new GetTableByIdQuery(id));
        return Results.Ok(response);
    }

    public async Task<IResult> GetAllTables(ISender sender)
    {
        var query = new GetAllTablesQuery();
        List<GetAllTablesResponse> response = await sender.Send(query);

        return Results.Ok(response);
    }

}
