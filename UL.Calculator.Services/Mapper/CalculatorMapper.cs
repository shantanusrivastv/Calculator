using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using UL.Calculator.Core.Model;
using UL.Calculator.Entities;

namespace UL.Calculator.Services.Mapper
{
    public class CalculatorMapper : Profile
    {
        public CalculatorMapper()
        {
            CreateMap<UserLogin, UserInfo>()
            .ForMember(dest => dest.Name,
                      opt => opt.MapFrom(userLogin => $"{userLogin.User.FirstName} {userLogin.User.LastName}"))

            .ForMember(dest => dest.Username,
                      opt => opt.MapFrom(userLogin => userLogin.Username))

            //.ForMember(dest => dest.SubscriptionType,
            //          opt => opt.MapFrom(userLogin => userLogin.SubscriptionType))

            .ForMember(dest => dest.Token,
                      opt => opt.MapFrom(userLogin => userLogin.Token));
        }
    }
}