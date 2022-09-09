using AutoMapper;
using MoneyMe.Api.Responses;
using MoneyMe.Application.Contracts.Dtos;

namespace MoneyMe.Api
{
    public class ApiMappingProfile : Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<FeeDto, FeeResponse>()
                .ConstructUsing(fee => new FeeResponse(
                    fee.Id.Value,
                    fee.DateCreated.Value,
                    fee.DateModified,
                    fee.Name,
                    fee.Amount.Value));
        }
    }
}