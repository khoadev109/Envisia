using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Application.Services
{
    public class StoreService : ServiceBase, IStoreService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StoreService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<IEnumerable<StoreDto>>> GetAllAsync()
        {
            ServiceResult<IEnumerable<StoreDto>> result = await ExecuteAsync<IEnumerable<StoreDto>>(async () =>
            {
                IEnumerable<Store> stores = await _unitOfWork.StoreRepository.GetQueryable().Include(x => x.Logo).ToListAsync();

                IEnumerable<StoreDto> storeDtos = _mapper.Map<IEnumerable<StoreDto>>(stores);

                return new ServiceSuccessResult<IEnumerable<StoreDto>>(storeDtos);
            });

            return result;
        }
    }
}
