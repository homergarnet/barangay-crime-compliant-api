
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{

    public interface IAuthService
    {

        UserDto CreateAccount(UserDto userReq);
        string Login(LoginDto loginInfo);

    }
}