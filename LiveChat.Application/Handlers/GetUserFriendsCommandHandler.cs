using LiveChat.Application.Models;
using LiveChat.Application.Queries;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class GetUserFriendsCommandHandler : IRequestHandler<GetUserFriendsQuery, IEnumerable<UserResponse>>
    {
        private readonly Dictionary<int, IEnumerable<UserResponse>> userFriends;
        private readonly object _lock;

        public GetUserFriendsCommandHandler(Dictionary<int, IEnumerable<UserResponse>> userFriends, object _lock)
        {
            this.userFriends = userFriends;
            this._lock = _lock;
        }

        public Task<IEnumerable<UserResponse>> Handle(GetUserFriendsQuery query, CancellationToken cancellationToken)
        {
            List<UserResponse> friends;

            lock (_lock)
            {
                if (userFriends.TryGetValue(query.CurrentUserId, out var userFriendsList))
                {
                    friends = new List<UserResponse>(userFriendsList);
                }
                else
                {
                    friends = new List<UserResponse>();
                }
            }

            return Task.FromResult(friends.AsEnumerable());
        }
    }
}
