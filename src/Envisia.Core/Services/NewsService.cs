using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;

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

        public async Task<ServiceResult<IEnumerable<NewsDto>>> GetByOrganisationIdAsync(int organisationId)
        {
            ServiceResult<IEnumerable<NewsDto>> result = await ExecuteAsync<IEnumerable<NewsDto>>(async () =>
            {
                IEnumerable<News> news = await _unitOfWork.NewsRepository.GetByOrganisationIdAsync(organisationId);

                IEnumerable<NewsDto> newsDtos = _mapper.Map<IEnumerable<NewsDto>>(news);

                return new ServiceSuccessResult<IEnumerable<NewsDto>>(newsDtos);
            });

            return result;
        }

        public async Task<ServiceResult<IEnumerable<NewsDto>>> GetByFormulaIdAsync(int formulaId)
        {
            ServiceResult<IEnumerable<NewsDto>> result = await ExecuteAsync<IEnumerable<NewsDto>>(async () =>
            {
                IEnumerable<News> news = await _unitOfWork.NewsRepository.GetByFormulaIdAsync(formulaId);

                IEnumerable<NewsDto> newsDtos = _mapper.Map<IEnumerable<NewsDto>>(news);

                return new ServiceSuccessResult<IEnumerable<NewsDto>>(newsDtos);
            });

            return result;
        }

        public async Task<ServiceResult<IEnumerable<NewsDto>>> GetByStoreIdAsync(int storeId)
        {
            ServiceResult<IEnumerable<NewsDto>> result = await ExecuteAsync<IEnumerable<NewsDto>>(async () =>
            {
                IEnumerable<News> news = await _unitOfWork.NewsRepository.GetByStoreIdAsync(storeId);

                IEnumerable<NewsDto> newsDtos = _mapper.Map<IEnumerable<NewsDto>>(news);

                return new ServiceSuccessResult<IEnumerable<NewsDto>>(newsDtos);
            });

            return result;
        }
    }
}
