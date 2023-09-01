using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;


namespace barangay_crime_compliant_api.Services
{
    public class AnnouncementService : IAnnouncementService
    {

        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;

        public AnnouncementService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public AnnouncementDto CreateAnnouncement(AnnouncementDto announcementRes)
        {

            Announcement announcement = new Announcement();

            announcement.Description = announcementRes.Description;
            announcement.UserId = announcementRes.UserId;
            announcement.DateTimeCreated = DateTime.Now;
            db.Announcements.Add(announcement);
            db.SaveChanges();
            return announcementRes;

        }

        public List<AnnouncementDto> GetAnnouncementList(string keyword, int page, int pageSize)
        {

            IQueryable<Announcement> query = db.Announcements;
            List<AnnouncementDto> announcementList = new List<AnnouncementDto>();
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.Description.Contains(keyword));
            }
            var announcementRes = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            foreach(var announcement in announcementRes)
            {
                var announcementDto = new AnnouncementDto();
                announcementDto.Id = announcement.Id;
                announcementDto.Description = announcement.Description;
                announcementDto.DateTimeCreated = announcement.DateTimeCreated;
                announcementDto.DateTimeUpdated = announcement.DateTimeUpdated;
                announcementDto.UserId = announcement.UserId;
                announcementList.Add(announcementDto);
            }
            return announcementList;

        }

        public AnnouncementDto UpdateAnnouncement(long id, long userId, AnnouncementDto announcementInfo)
        {

            var hasAnnouncement = db.Announcements.Any(z => z.Id == id && z.UserId == userId);
            if(hasAnnouncement) 
            {

                var announcement = db.Announcements.Where(z => z.Id == id && z.UserId == userId).First();
                announcement.Description = announcementInfo.Description;
                announcement.DateTimeUpdated = DateTime.Now;
                db.SaveChanges();

            }
            
            
            return announcementInfo;
        }

        public string SoftRemoveAnnouncement(long id, long userId)
        {

            var hasAnnouncement = db.Announcements.Any(z => z.Id == id && z.UserId == userId);
            if(hasAnnouncement) 
            {

                var announcement = db.Announcements.Where(z => z.Id == id && z.UserId == userId).First();
                announcement.DateTimeUpdated = DateTime.Now;
                announcement.IsActive = false;
                db.SaveChanges();

            }

            return "Announcement has been deleted";
            
            
        }

        
        
    }
}