using ReportService.API.Domain.Entities;

namespace ReportService.API.Infrastructure.Repository
{
    public interface IReportRepository
    {
        Task<Report> CreateReportAsync();
        Task<IList<Report>> GetAllReportsAsync();
        Task<Report> GetReportByIdAsync(string id);
        Task ReportCompletedAsync(string id);
        Task<IList<ReportDetail>> GetDetailsByReportIdAsync(string reportId);
        Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails);
    }
}
