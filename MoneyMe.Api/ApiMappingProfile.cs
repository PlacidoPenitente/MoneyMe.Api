using AutoMapper;
using MoneyMe.Api.Models;
using MoneyMe.Application.Contracts.Dtos;

namespace MoneyMe.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<FeeDto, Fee>().ConstructUsing(x => new Fee(x.Id, x.DateAdded, x.DateModified));
        }
    }
}