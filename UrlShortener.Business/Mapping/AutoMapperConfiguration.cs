using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UrlShortener.Business.Domain;
using UrlShortener.Business.UI.Models;

namespace UrlShortener.Business.Mapping
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            // UrlController ViewModel Mappings
            // Index
            Mapper.CreateMap<ShortLink, NewShortLink>();
            Mapper.CreateMap<ShortLink, PopularShortLink>();
            // Create
            Mapper.CreateMap<UrlCreateViewModel, ShortLink>();
            // Delete
            Mapper.CreateMap<ShortLink, UrlDeleteViewModel>();
            // Edit
            Mapper.CreateMap<ShortLink, UrlEditViewModel>();
            // Details
            Mapper.CreateMap<ShortLink, UrlDetailsViewModel>();
        }
    }
}
