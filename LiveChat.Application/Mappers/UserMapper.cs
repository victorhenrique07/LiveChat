using AutoMapper;
using LiveChat.Application.Models;
using LiveChat.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Mappers
{
    public class UserMapper : AutoMapper.Profile
    {
        public UserMapper() 
        {
            CreateMap<User, UserResponse>();
        }
    }
}
