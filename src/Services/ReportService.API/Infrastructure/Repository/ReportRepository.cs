using MongoDB.Driver;
using ReportService.API.Configurations.Settings;
using ReportService.API.Domain.Entities;

namespace ReportService.API.Infrastructure.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly IMongoCollection<Report> _reportCollection;
        private readonly IMongoCollection<ReportDetail> _reportDetailCollection;

        public ReportRepository(IDatabaseSettings databaseSettings)
        {
            var client = new MongoClient(databaseSettings.ConnectionString);
            var db = client.GetDatabase(databaseSettings.DatabaseName);

            _reportCollection = db.GetCollection<Report>(databaseSettings.ReportCollectionName);
            _reportDetailCollection = db.GetCollection<ReportDetail>(databaseSettings.ReportDetailCollectionName);
        }

        public async Task<Report> CreateReportAsync()
        {
            var newReport = new Report(DateTime.UtcNow, Report.ReportStatus.Preparing);

            await _reportCollection.InsertOneAsync(newReport);

            return newReport;
        }

        public async Task<IList<Report>> GetAllReportsAsync()
        {
            return await _reportCollection.Find(x => true)
              .ToListAsync();
        }

        public async Task<Report> GetReportByIdAsync(string id)
        {
            return await _reportCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IList<ReportDetail>> GetDetailsByReportIdAsync(string reportId)
        {
            return await _reportDetailCollection.Find(x => x.ReportId == reportId).ToListAsync();
        }

        public async Task CreateReportDetailsAsync(IList<ReportDetail> reportDetails)
        {
            await _reportDetailCollection.InsertManyAsync(reportDetails);
        }

        public async Task ReportCompletedAsync(string id)
        {
            var filter = Builders<Report>.Filter.Eq(s => s.Id, id);
            var update = Builders<Report>.Update
              .Set(s => s.Status, Report.ReportStatus.Completed)
              .Set(s => s.CompletedDate, DateTime.UtcNow);

            await _reportCollection.UpdateOneAsync(filter, update);
        }
    }
}
