using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{
    public interface IAnnouncementService
    {

        AnnouncementDto CreateAnnouncement(AnnouncementDto announcementRes);
        List<AnnouncementDto> GetAnnouncementList(string keyword, int page, int pageSize);
        AnnouncementDto UpdateAnnouncement(long id, long userId, AnnouncementDto announcementInfo);
        string SoftRemoveAnnouncement(long id, long userId);

    }
}