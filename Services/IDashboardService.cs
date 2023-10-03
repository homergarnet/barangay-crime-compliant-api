using barangay_crime_compliant_api.DTOS;

public interface IDashboardService
{
    DashboardDto TotalDashboardCardCount(long userId, string barangayCode = "");
}