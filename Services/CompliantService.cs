using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;


namespace barangay_crime_compliant_api.Services
{
    public class CompliantService : ICompliantService
    {
        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public CompliantService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public async Task<List<long>> CreateCaseReport(List<IFormFile> CrimeImage, long UserId, string CompliantType, string Description, DateTime DateTimeCreated, string CaseType)
        {
            
            var uploadedImageIds = new List<long>();
            var crimeCompliantReport = new CrimeCompliantReport
            {
                CompliantType = CompliantType,
                Description = Description,
                DateTimeCreated = DateTime.UtcNow,
                CaseType = CaseType,
                UserId = UserId,
            };
            await db.CrimeCompliantReports.AddAsync(crimeCompliantReport);
            await db.SaveChangesAsync();
            foreach (var imageFile in CrimeImage)
            {

                if (imageFile.Length == 0)
                {
                    continue;
                }

                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    var crimeImageReport = new CrimeImage
                    {

                        CrimeCompliantReportId = crimeCompliantReport.Id,
                        Image = memoryStream.ToArray(),
                        DateTimeCreated = DateTime.UtcNow,
                        FileName = imageFile.FileName,

                    };

                    await db.CrimeImages.AddAsync(crimeImageReport);
                    await db.SaveChangesAsync();

                    uploadedImageIds.Add(crimeImageReport.Id);

                }
                
            }

            return uploadedImageIds;

        }
    }
}