using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AspnetCore6ApiTestingDemo.Test
{
    [TestFixture]
    public abstract class TestBase
    {
        private IServiceScope scope;
        private readonly WebApplicationFactory<Program> _webApplicationFactory;
        protected HttpClient Client { get; private set; }

        protected IServiceProvider ServiceProvider { get; private set; }

        protected readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        protected TestBase(WebApplicationFactory<Program> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [OneTimeSetUp]
        public void GlobalInit()
        {
            Client = _webApplicationFactory.CreateClient();
        }

        [OneTimeTearDown]
        public void GlobalTearDown()
        {
            Client?.Dispose();
            _webApplicationFactory?.Dispose();
        }

        [SetUp]
        protected void Init()
        {
            scope = _webApplicationFactory.Services.CreateScope();
            ServiceProvider = scope.ServiceProvider;
        }

        [TearDown]
        protected void Teardown()
        {
            scope?.Dispose();
        }
    }
}