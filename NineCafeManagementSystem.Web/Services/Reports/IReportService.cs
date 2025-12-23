namespace NineCafeManagementSystem.Web.Services.Reports
{
    public interface IReportService
    {
        Task<MonthlyReportVM> GetMonthlyReportAsync(int year, int month);
        Task<byte[]> GenerateMonthlyReportExcelAsync(int year, int month);
    }
}
