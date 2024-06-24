using LiveChat.Application.Commands;
using LiveChat.Domain.Models;
using LiveChat.Infraestructure.Repository.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository studentRepository)
        {
            _userRepository = studentRepository;
        }
        public async Task<User> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var studentDetails = new User()
            {
                Name = command.Name,
                Email = command.Email
            };

            return await _userRepository.AddUserAsync(studentDetails);
        }
    }
}
