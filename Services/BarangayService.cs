using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public class BarangayService : IBarangayService
    {
        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public BarangayService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public List<BarangayDto> GetBarangayNameByCodeList(string keyword, int page, int pageSize)
        {

            IQueryable<PhBrgy> query = db.PhBrgies;
            List<BarangayDto> barangayList = new List<BarangayDto>();
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.BrgyCode.Contains(keyword));
            }

            var barangayRes = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach(var barangay in barangayRes)
            {

                var barangayDto = new BarangayDto();
                barangayDto.BarangayCode = barangay.BrgyCode;
                barangayDto.BarangayName = barangay.BrgyName;
                barangayDto.CityCode = barangay.CityCode;
                barangayDto.ZipCode = barangay.ZipCode;
                barangayList.Add(barangayDto);
                
            }
            
            return barangayList;
            
        }
    }
}