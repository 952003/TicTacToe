using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.Models;
using TicTacToe.ViewModels;

namespace TicTacToe.MappingProfile
{
    public class SessionDataProfile : Profile
    {
        public SessionDataProfile()
        {
            CreateMap<SessionData, SessionVM>().ForMember(vm => vm.UserName, opt => opt.MapFrom(m => m.Creator.Login));
        }
    }
}
