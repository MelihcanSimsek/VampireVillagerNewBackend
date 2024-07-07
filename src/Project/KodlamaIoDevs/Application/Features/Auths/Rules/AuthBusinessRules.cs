using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;

namespace Application.Features.Auths.Rules
{
    public class AuthBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public AuthBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task EmailCanNotBeDuplicatedWhenRegistered(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            if (user != null) throw new BusinessException("Email already exists");
        }

        public async Task CheckUserAlreadyExistsWhenLogged(string email)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            if (user == null) throw new BusinessException("User not found");
        }

        public async Task CheckUserPasswordIsCorrectWhenLogged(string email,string password)
        {
            User? user = await _userRepository.GetAsync(u => u.Email == email);
            bool isVerify = HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            if (!isVerify) throw new BusinessException("Password is not correct");
        }
    }
}
