using LiveChat.Application.Commands;
using LiveChat.Infraestructure.Repository.Users;
using MediatR;

namespace LiveChat.Application.Handlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<int> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByIdAsync(command.Id);
            if (user == null)
                return default;

            user.Name = command.Name;
            user.Email = command.Email;

            return await _userRepository.UpdateUserAsync(user);
        }
    }
}
