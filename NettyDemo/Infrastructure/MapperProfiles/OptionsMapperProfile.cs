using AutoMapper;
using NettyDemo.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zaabee.RabbitMQ;

namespace NettyDemo.Infrastructure.MapperProfiles
{
    public class OptionsMapperProfile : Profile
    {
        public OptionsMapperProfile()
        {
            CreateMap<RabbitMqOptions, MqConfig>()
                .ForMember(config => config.HeartBeat, opt => opt.MapFrom(options => TimeSpan.Parse(options.HeartBeat)))
                .ForMember(config => config.NetworkRecoveryInterval, opt => opt.MapFrom(options => TimeSpan.Parse(options.NetworkRecoveryInterval)));
        }
    }
}
