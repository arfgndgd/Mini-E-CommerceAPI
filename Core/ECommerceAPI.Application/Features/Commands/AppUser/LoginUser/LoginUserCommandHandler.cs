using ECommerceAPI.Application.Abstractions.Token;
using ECommerceAPI.Application.DTOs;
using ECommerceAPI.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A = ECommerceAPI.Domain.Entities.Identity;

namespace ECommerceAPI.Application.Features.Commands.AppUser.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        readonly UserManager<A.AppUser> _userManager;
        readonly SignInManager<A.AppUser> _signInManager;
        readonly ITokenHandler _tokenHandler;
        public LoginUserCommandHandler(UserManager<A.AppUser> userManager, SignInManager<A.AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            A.AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if (user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail);
            }

            if (user == null)
            {
                throw new NotFoundUserException();
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (result.Succeeded) //Authentication başarılı
            {
                // Authorization Yetkilerin belirlenmesi gerekiyor
                Token token = _tokenHandler.CreateAccessToken(5);
                return new LoginUserSuccessCommandResponse()
                {
                    Token = token
                };
            };
            //return new LoginUserErrorCommandResponse()
            //{
            //    Message = "Kullanıcı adı veya şifre hatalı"
            //};
            throw new AuthenticationErrorException();
        }
    }
}
