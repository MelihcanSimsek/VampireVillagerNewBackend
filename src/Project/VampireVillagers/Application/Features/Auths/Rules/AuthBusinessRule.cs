using Application.Features.Auths.Constants;
using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            User? user =await _userRepository.GetAsync(p => p.Email == email);
            if (user != null) throw new BusinessException(Messages.EmailAlreadyExists);
        }

        public async Task CheckUserAlredyExistsWhenLogged(string email)
        {
            User? user = await _userRepository.GetAsync(p => p.Email == email);
            if (user == null) throw new BusinessException(Messages.UserNotFound);
        }

        public async Task CheckUserPasswordCorrectWhenUserLogged(string email,string password)
        {
            User? user = await _userRepository.GetAsync(p => p.Email == email);
            bool isCorrect = HashingHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
            if (!isCorrect) throw new BusinessException(Messages.PasswordIsNotCorrect);
        }
    }
}
