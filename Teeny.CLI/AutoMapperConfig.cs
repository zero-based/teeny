using AutoMapper;
using Teeny.Core.Parse;

namespace Teeny.CLI
{
    public class AutoMapperConfig
    {
        public static Mapper Mapper
        {
            get
            {
                var config = new MapperConfiguration(Configure);
                return new Mapper(config);
            }
        }

        private static void Configure(IMapperConfigurationExpression cfg)
        {
            cfg.CreateMap<Node, TreeNode>()
                .ForMember(dest => dest.Name, act => act.MapFrom(src => src.Name))
                .ForMember(dest => dest.ChildNodes, act => act.MapFrom(src => src.Children));
        }
    }
}