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

        public async Task<List<long>> CreateCaseReport(List<IFormFile> CrimeImage, long UserId, string Description, DateTime DateTimeCreated, long CrimeCompliantId)
        {
            
            var uploadedImageIds = new List<long>();
            var crimeCompliantReport = new CrimeCompliantReport
            {

                Description = Description,
                DateTimeCreated = DateTime.Now,
                CrimeCompliantId = CrimeCompliantId,
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
                // Define a target directory to save the uploaded file
                var targetDirectory = "uploads"; // Change this to your desired directory
                var filePath = Path.Combine(targetDirectory, Guid.NewGuid().ToString() + "_" + imageFile.FileName);
                Directory.CreateDirectory(targetDirectory); // Ensure the directory exists
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                    var crimeImageReport = new CrimeImage
                    {

                        CrimeCompliantReportId = crimeCompliantReport.Id,
                        Image = imageFile.FileName,
                        DateTimeCreated = DateTime.Now,
                        FileName = imageFile.FileName,

                    };

                    await db.CrimeImages.AddAsync(crimeImageReport);
                    await db.SaveChangesAsync();

                    uploadedImageIds.Add(crimeImageReport.Id);
                }
                
            }

            return uploadedImageIds;

        }

        public List<CrimeCompliantDto> GetCrimeCompliantList(string keyword, int page, int pageSize)
        {

            IQueryable<CrimeCompliant> query = db.CrimeCompliants;
            List<CrimeCompliantDto> crimeCompliantList = new List<CrimeCompliantDto>();
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.Title.Contains(keyword));
            }

            var crimeCompliantRes = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach(var crimeCompliant in crimeCompliantRes)
            {

                var crimeCompliantDto = new CrimeCompliantDto();
                crimeCompliantDto.Id = crimeCompliant.Id;
                crimeCompliantDto.Title = crimeCompliant.Title;
                crimeCompliantDto.Type = crimeCompliant.Type;
                crimeCompliantList.Add(crimeCompliantDto);
                
            }
            
            return crimeCompliantList;

        }


    }
}