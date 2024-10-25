using Application.Features.Tables.Commands.Create;
using Application.Features.Tables.Commands.Update;
using Application.Features.Tables.Queries.GetAllList;
using Application.Features.Tables.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Tables.Profiles;

public sealed class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Table, CreatedTableResponse>().ReverseMap();
        CreateMap<Table, CreateTableCommand>().ReverseMap();
        CreateMap<Table, UpdateTableCommand>().ReverseMap();
        CreateMap<Table, GetTableByIdResponse>().ReverseMap();
        CreateMap<Table, GetTableByIdQuery>().ReverseMap();
        CreateMap<Table, GetAllTablesResponse>();
    }
}