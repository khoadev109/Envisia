using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Application.Services
{
    public class OrganisationService : ServiceBase, IOrganisationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrganisationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<IEnumerable<OrganisationDto>>> GetAllAsync()
        {
            ServiceResult<IEnumerable<OrganisationDto>> result = await ExecuteAsync<IEnumerable<OrganisationDto>>(async () =>
            {
                IEnumerable<Organisation> organisations = await _unitOfWork.OrganisationRepository.GetQueryable().Include(x => x.Logo).ToListAsync();

                IEnumerable<OrganisationDto> organisationDtos = _mapper.Map<IEnumerable<OrganisationDto>>(organisations);

                return new ServiceSuccessResult<IEnumerable<OrganisationDto>>(organisationDtos);
            });

            return result;
        }
    }
}
