using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Application.Services
{
    public class NewsService : ServiceBase, INewsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public NewsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<IEnumerable<NewsDto>>> GetAllAsync()
        {
            ServiceResult<IEnumerable<NewsDto>> result = await ExecuteAsync<IEnumerable<NewsDto>>(async () =>
            {
                IEnumerable<News> news =  await _unitOfWork.NewsRepository.GetQueryable()
                    .Include(x => x.Store)
                    .Include(x => x.Formula)
                    .Include(x=> x.Feed)
                    .ToListAsync();

                IEnumerable<NewsDto> newsDtos = _mapper.Map<IEnumerable<NewsDto>>(news);

                return new ServiceSuccessResult<IEnumerable<NewsDto>>(newsDtos);
            });

            return result;
        }
    }
}
