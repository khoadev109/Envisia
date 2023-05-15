using AutoMapper;
using Envisia.Application.Dtos;
using Envisia.Application.Interfaces.Services;
using Envisia.Data.Entities;
using Envisia.Data.Interfaces;
using Envisia.Library;
using Envisia.Library.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Envisia.Application.Services
{
    public class UserService : ServiceBase, IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServiceResult<bool>> CheckPasswordAsync(string userName, string password)
        {
            ServiceResult<bool> result = await ExecuteAsync<bool>(async () =>
            {
                User user = await _unitOfWork.UserRepository.GetAsync(u => u.UserName == userName);

                if (user == null)
                {
                    return new ServiceFailResult<bool>();
                }

                var isValidPassword = PasswordHelper.VerifyPassword(password, user.PasswordHash, user.PasswordSalt);

                return new ServiceSuccessResult<bool>(isValidPassword);
            });

            return result;
        }

        public async Task<ServiceResult<UserDto>> FindByUserNameAsync(string userName)
        {
            ServiceResult<UserDto> result = await ExecuteAsync<UserDto>(async () =>
            {
                User user = await _unitOfWork.UserRepository.GetAsync(x => x.UserName == userName);

                if (user == null)
                {
                    return new ServiceFailResult<UserDto>();
                }

                UserDto userDto = _mapper.Map<UserDto>(user);

                return new ServiceSuccessResult<UserDto>(userDto);
            });

            return result;
        }

        public async Task<ServiceResult<bool>> SaveTenantRefreshTokenAsync(RefreshTokenDto dto)
        {
            ServiceResult<bool> result = await ExecuteAsync<bool>(async () =>
            {
                RefreshToken existingRefreshToken = await _unitOfWork.RefreshTokenRepository.GetQueryable().SingleOrDefaultAsync();

                if (existingRefreshToken != null)
                {
                    _unitOfWork.RefreshTokenRepository.Remove(existingRefreshToken);
                }

                RefreshToken refreshToken = _mapper.Map<RefreshToken>(dto);

                await _unitOfWork.RefreshTokenRepository.AddAsync(refreshToken);

                await _unitOfWork.SaveAsync();

                return new ServiceSuccessResult<bool>(true);
            });

            return result;
        }

        public async Task<ServiceResult<RefreshTokenDto>> GetRefreshTokenAsync()
        {
            ServiceResult<RefreshTokenDto> result = await ExecuteAsync<RefreshTokenDto>(async () =>
            {
                RefreshToken existingRefreshToken = await _unitOfWork.RefreshTokenRepository.GetQueryable().SingleOrDefaultAsync();

                if (existingRefreshToken == null)
                {
                    return new ServiceFailResult<RefreshTokenDto>();
                }

                RefreshTokenDto refreshTokenDto = _mapper.Map<RefreshTokenDto>(existingRefreshToken);

                return new ServiceSuccessResult<RefreshTokenDto>(refreshTokenDto);
            });

            return result;
        }
    }
}
