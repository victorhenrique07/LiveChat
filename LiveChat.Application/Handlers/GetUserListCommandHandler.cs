using LiveChat.Application.Queries;
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
    public class GetUserListCommandHandler : IRequestHandler<GetUserListQuery, List<User>>
    {
        private readonly IUserRepository _studentRepository;

        public GetUserListCommandHandler(IUserRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<User>> Handle(GetUserListQuery query, CancellationToken cancellationToken)
        {
            return await _studentRepository.GetUserListAsync();
        }
    }
}
