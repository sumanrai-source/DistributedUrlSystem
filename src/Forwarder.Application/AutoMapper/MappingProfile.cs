using Assigner.Domain.Entities;
using AutoMapper;
using Forwarder.Application.Forwarder.Queries.GetAvailableSlug;
using Shared.Contracts.SlugData;
using System;
using System.Collections.Generic;
using System.Text;

namespace Forwarder.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Slugs, GetAvailableSlugsResponse>().ReverseMap();
            CreateMap<SlugDto, GetAvailableSlugsResponse>().ReverseMap();

            CreateMap<SlugDto, GetAvailableSlugsResponse>()
            .ForMember(
                dest => dest.slug,
                opt => opt.MapFrom(src => src.Slug))
            .ForMember(
                dest => dest.status,
                opt => opt.MapFrom(src => src.Status));
                }
    }
}
