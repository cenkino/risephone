using AutoMapper;
using ReportService.API.Domain.Entities;
using ReportService.API.Models;

namespace ReportService.API.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Report, ReportVal>().ReverseMap();
            CreateMap<Report, ReportIndexVal>().ReverseMap();
            CreateMap<ReportDetail, ReportDetailsVal>().ReverseMap();
        }
    }
}
