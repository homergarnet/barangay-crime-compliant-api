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

        public AnnouncementDescription CreateAnnouncement(AnnouncementDescription announcementDescriptionInfo)
        {

            Announcement announcement = new Announcement();
            announcement.Description = announcementDescriptionInfo.Description;
            announcement.UserId = announcementDescriptionInfo.UserId;
            announcement.DateTimeCreated = DateTime.Now;
            db.Announcements.Add(announcement);
            db.SaveChanges();
            return announcementDescriptionInfo;

        }

        public AnnouncementDto GetAnnouncement(string keyword, int page, int pageSize)
        {

            IQueryable<Announcement> query = db.Announcements;
            long announcementTotalCount = query.Count();
            AnnouncementDto announcementResponse = new AnnouncementDto();
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(z => z.Description.Contains(keyword));
            }
            var announcementRes = query.OrderByDescending(z => z.Id).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            var announcementDescriptionList = new List<AnnouncementDescription>();
            foreach(var announcement in announcementRes)
            {
                AnnouncementDescription announcementDescriptionDto = new AnnouncementDescription();
                announcementDescriptionDto.Id = announcement.Id;
                announcementDescriptionDto.Description = announcement.Description;
                announcementDescriptionDto.DateTimeCreated = announcement.DateTimeCreated;
                announcementDescriptionDto.DateTimeUpdated = announcement.DateTimeUpdated;
                announcementDescriptionDto.UserId = announcement.UserId;
                announcementDescriptionList.Add(announcementDescriptionDto);
            }
            announcementResponse.AnnouncementDescription = announcementDescriptionList;
            announcementResponse.AnnouncementTotalCount = announcementTotalCount;
            return announcementResponse;

        }

        public AnnouncementDescription UpdateAnnouncement(long id, long userId, AnnouncementDescription announcementInfo)
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