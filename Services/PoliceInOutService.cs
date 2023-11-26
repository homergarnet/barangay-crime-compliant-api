using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using Microsoft.EntityFrameworkCore;

namespace barangay_crime_compliant_api.Services
{
    public class PoliceInOutService : IPoliceInOutService
    {
        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public PoliceInOutService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public PoliceInOutDto CreatePoliceInOut(PoliceInOutDto policeInOutReq)
        {

            PoliceInOut policeInOut = new PoliceInOut();
            policeInOut.UserId = policeInOutReq.UserId;
            policeInOut.Type = policeInOutReq.Type;
            policeInOut.DateTimeCreated = DateTime.Now;
            db.PoliceInOuts.Add(policeInOut);
            db.SaveChanges();
            return policeInOutReq;

        }

        public List<PoliceInOutDto> GetPoliceInOutList(string keyword, int page, int pageSize)
        {

            IQueryable<PoliceInOut> query = db.PoliceInOuts;
            List<PoliceInOutDto> policeInOutList = new List<PoliceInOutDto>();
            // DateTime dateTimeParsed = !string.IsNullOrEmpty(keyword) ?  DateTime.Parse(keyword) : DateTime.Parse("2023-11-26");
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.User.FirstName.Contains(keyword) || z.User.MiddleName.Contains(keyword) || z.User.LastName.Contains(keyword) || z.Type.Contains(keyword));
            }

            var policeInOutRes = query.Include(z => z.User).Skip((page - 1) * pageSize).Take(pageSize).ToList();

            foreach (var policeInOut in policeInOutRes)
            {

                var policeInOutDto = new PoliceInOutDto();
                policeInOutDto.Id = policeInOut.Id;
                policeInOutDto.UserId = policeInOut.UserId;
                policeInOutDto.Name = policeInOut.User.FirstName + " " + policeInOut.User.MiddleName + " " + policeInOut.User.LastName;
                policeInOutDto.Type = policeInOut.Type;
                policeInOutDto.DateTimeCreated = policeInOut.DateTimeCreated.Value.ToString("yyyy-MM-dd HH:mm:ss");
                policeInOutList.Add(policeInOutDto);

            }

            return policeInOutList;

        }

    }
}