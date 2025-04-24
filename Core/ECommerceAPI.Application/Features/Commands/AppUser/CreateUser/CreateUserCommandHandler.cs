using A = ECommerceAPI.Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECommerceAPI.Application.Exceptions;

namespace ECommerceAPI.Application.Features.Commands.AppUser.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        readonly UserManager<A.AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<A.AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                UserName = request.Username,
                Email = request.Email,
                NameSurname = request.NameSurname
            }, request.Password);

            if (result.Succeeded)
            {
                return new()
                {
                    Succeeded = true,
                    Message = "Kullanıcı başarıyla oluşturulmuştur."
                };
            }
            throw new UserCreateFailedException();
        }
    }
}
