using LiveChat.Application.Commands;
using LiveChat.Application.Exceptions;
using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using LiveChat.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Handlers
{
    public class AddFriendCommandHandler : IRequestHandler<AddFriendCommand, UserResponse>
    {
        private readonly Dictionary<int, IEnumerable<UserResponse>> userFriends;
        private readonly object _lock;

        private readonly IUserRepository userRepository;

        public AddFriendCommandHandler(IUserRepository userRepository, object _lock, Dictionary<int, IEnumerable<UserResponse>> userFriends)
        {
            this.userRepository = userRepository;
            this._lock = _lock;
            this.userFriends = userFriends;
        }

        public async Task<UserResponse> Handle(AddFriendCommand command, CancellationToken cancellationToken)
        {
            User friend = await userRepository.GetUserByEmailAsync(command.Email);
            UserResponse newFriend = new UserResponse();

            if (friend == null)
            {
                throw new NotFoundException(HttpStatusCode.NotFound, "User not found.");
            }

            List<UserResponse> updatedFriendsCollection;

            lock (_lock)
            {
                if (userFriends.TryGetValue(command.CurrentUserid, out IEnumerable<UserResponse> friendsCollection))
                {
                    if (friendsCollection.Any(u => u.Email == friend.Email))
                    {
                        throw new Exception("This user is already a friend");
                    }

                    newFriend = new UserResponse() { Email = friend.Email, Name = friend.Name };

                    updatedFriendsCollection = new List<UserResponse>(friendsCollection) { newFriend };
                }
                else
                {
                    newFriend = new UserResponse() { Email = friend.Email, Name = friend.Name };

                    updatedFriendsCollection = new List<UserResponse> { newFriend };
                }

                userFriends[command.CurrentUserid] = updatedFriendsCollection.AsReadOnly();
            }

            return newFriend;
        }
    }
}
