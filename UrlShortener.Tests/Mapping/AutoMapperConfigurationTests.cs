using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using NUnit.Framework;
using UrlShortener.Business.Mapping;

namespace UrlShortener.Tests.Mapping
{
    [TestFixture]
    public class AutoMapperConfigurationTests
    {
        [Test]
        public void AutoMapperConfiguration_Configure_Succeeds()
        {
            // Arrange
            AutoMapperConfiguration.Configure();

            // Act

            // Assert
            Mapper.AssertConfigurationIsValid();
        }
    }
}
