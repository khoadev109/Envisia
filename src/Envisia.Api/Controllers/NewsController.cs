using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Library;
using Microsoft.AspNetCore.Mvc;

namespace Envisia.Api.Controllers
{
    public class NewsController : EnvisiaControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }
    }
}
