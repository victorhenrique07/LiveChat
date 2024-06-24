using MediatR;
using LiveChat.Domain.Models;

namespace LiveChat.Application.Queries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public int Id { get; set; }
    }
}
