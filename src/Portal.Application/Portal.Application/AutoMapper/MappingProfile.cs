using AutoMapper;
using Portal.Application.Portal.Command.CreateShortUrl;
using Portal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Portal.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region UrlShort

            CreateMap<CreateShortUrlResponse, UrlMapping>().ReverseMap();

            #endregion
        }
    }
}
