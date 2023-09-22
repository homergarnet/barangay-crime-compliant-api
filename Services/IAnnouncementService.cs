using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface IAnnouncementService
    {

        AnnouncementDescription CreateAnnouncement(AnnouncementDescription announcementDescriptionInfo);
        AnnouncementDto GetAnnouncement(string keyword, int page, int pageSize);
        AnnouncementDescription UpdateAnnouncement(long id, long userId, AnnouncementDescription announcementInfo);
        string SoftRemoveAnnouncement(long id, long userId);

    }
}