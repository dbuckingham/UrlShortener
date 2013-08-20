using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using UrlShortener.Business;
using UrlShortener.Business.Mapping;
using UrlShortener.Business.UI.Models;

namespace UrlShortener.App_Start
{
    public class MapperConfig
    {
        public static void RegisterMappings()
        {
            AutoMapperConfiguration.Configure();
        }
    }
}