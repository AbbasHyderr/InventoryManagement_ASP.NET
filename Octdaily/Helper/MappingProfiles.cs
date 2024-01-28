using AutoMapper;
using Octdaily.Dto;
using Octdaily.Models;

namespace Octdaily.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        }
    }
}
