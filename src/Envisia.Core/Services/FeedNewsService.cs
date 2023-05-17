using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Application.Services
{
    public class FeedNewsService : ServiceBase, IFeedNewsService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FeedNewsService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<IEnumerable<NewsDto>>> GetAllNewsAsync()
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

        public async Task<ServiceResult<IEnumerable<FeedDto>>> GetAllFeedsAsync()
        {
            ServiceResult<IEnumerable<FeedDto>> result = await ExecuteAsync<IEnumerable<FeedDto>>(async () =>
            {
                IEnumerable<Feed> feeds = await _unitOfWork.FeedRepository.GetQueryable()
                    .Include(x => x.NewsList.Take(10)).ThenInclude(x => x.Store)
                    .Include(x => x.NewsList.Take(10)).ThenInclude(x => x.Formula)
                    .ToListAsync();

                IEnumerable<FeedDto> newsDtos = _mapper.Map<IEnumerable<FeedDto>>(feeds);

                return new ServiceSuccessResult<IEnumerable<FeedDto>>(newsDtos);
            });

            return result;
        }
    }
}
