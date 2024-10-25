using Application.Features.Reservations.Commands.Create;
using Application.Features.Reservations.Commands.Update;
using Application.Features.Reservations.Commands.UpdateConfirmation;
using Application.Features.Reservations.Queries.GetAllList;
using Application.Features.Reservations.Queries.GetById;
using AutoMapper;
using Domain.Entities;

namespace Application.Features.Reservations.Profiles;

public sealed class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Reservation, CreateReservationCommand>().ReverseMap();
        CreateMap<Reservation, UpdateReservationCommand>().ReverseMap();
        CreateMap<Reservation, UpdateConfirmationCommand>().ReverseMap();
        CreateMap<Reservation, GetReservationByIdResponse>().ReverseMap();
        CreateMap<Reservation, GetReservationByIdQuery>().ReverseMap();
        CreateMap<Reservation, GetAllReservationsResponse>().ReverseMap();
    }
}