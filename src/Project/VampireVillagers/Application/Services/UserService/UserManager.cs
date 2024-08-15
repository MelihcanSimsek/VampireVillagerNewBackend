using Application.Services.Repositories;
using Core.Security.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.UserService
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserById(Guid id)
        {
            User? user = await _userRepository.GetAsync(p => p.Id == id);
            return user;
        }
    }
}
