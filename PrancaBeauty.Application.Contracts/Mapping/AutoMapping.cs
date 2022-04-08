using AutoMapper;
using System;
using System.Globalization;

namespace PrancaBeauty.Application.Contracts.Mapping
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<DateTime, string>().ConvertUsing(a => a.ToString("yyyy-MM-dd HH:mm"));
            CreateMap<double, string>().ConvertUsing(a => a.ToString(new CultureInfo("en-US")));
        }
    }
}
