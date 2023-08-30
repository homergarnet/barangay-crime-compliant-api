
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;

namespace barangay_crime_compliant_api.Services
{

    public interface IAuthService
    {

        UserDto CreateAccount(UserDto userReq);
        string CreatePersonalInfo(
            IFormFile ValidId, IFormFile SelfieId, string Username, string Password, string FirstName, 
            string MiddleName, string LastName, DateTime BirthDate, string Gender, string Phone, 
            string HouseNo, string Street, string Village, string UnitFloor, string Building, 
            string ProvinceCode, string CityCode, string BrgyCode, string ZipCode, DateTime DateCreated, 
            string UserType
        );
        string Login(LoginDto loginInfo);

    }
}