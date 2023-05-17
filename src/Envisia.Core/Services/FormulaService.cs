using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Application.Services
{
    public class FormulaService : ServiceBase, IFormulaService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FormulaService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<IEnumerable<FormulaDto>>> GetAllAsync()
        {
            ServiceResult<IEnumerable<FormulaDto>> result = await ExecuteAsync<IEnumerable<FormulaDto>>(async () =>
            {
                
                IEnumerable<Formula> formulas = await _unitOfWork.FormulaRepository.GetQueryable().Include(x => x.Logo).ToListAsync();

                IEnumerable<FormulaDto> formulaDtos = _mapper.Map<IEnumerable<FormulaDto>>(formulas);

                return new ServiceSuccessResult<IEnumerable<FormulaDto>>(formulaDtos);
            });

            return result;
        }
    }
}
