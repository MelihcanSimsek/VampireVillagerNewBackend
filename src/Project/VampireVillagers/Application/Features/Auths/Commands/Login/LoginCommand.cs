using Application.Features.Auths.Dtos;
using Application.Features.Auths.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;

namespace Application.Features.Auths.Commands.Login
{
    public class LoginCommand:IRequest<LoggedDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }
        public string IpAddress { get; set; }

        public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly AuthBusinessRules _authBusinessRules;
            private readonly IAuthService _authService;

            public LoginCommandHandler(IUserRepository userRepository, AuthBusinessRules authBusinessRules, IAuthService authService)
            {
                _userRepository = userRepository;
                _authBusinessRules = authBusinessRules;
                _authService = authService;
            }

            public async Task<LoggedDto> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                await _authBusinessRules.CheckUserAlredyExistsWhenLogged(request.UserForLoginDto.Email);
                await _authBusinessRules.CheckUserPasswordCorrectWhenUserLogged(request.UserForLoginDto.Email, request.UserForLoginDto.Password);

                User? user = await _userRepository.GetAsync(p => p.Email == request.UserForLoginDto.Email);

                AccessToken accessToken = await _authService.CreateAccessToken(user);
                RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(user, request.IpAddress);
                RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

                LoggedDto loggedDto = new() { AccessToken = accessToken, RefreshToken = addedRefreshToken };
                return loggedDto;
            }
        }

    }
}
