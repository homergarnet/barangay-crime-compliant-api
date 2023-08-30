using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using barangay_crime_complaint_api.Models;
using barangay_crime_compliant_api.DTOS;
using Microsoft.IdentityModel.Tokens;

namespace barangay_crime_compliant_api.Services
{
    public class AuthService : IAuthService
    {

        private readonly IConfiguration configuration;
        private readonly Thesis_CrimeContext db;
        public AuthService(IConfiguration configuration, Thesis_CrimeContext db)
        {
            this.configuration = configuration;
            this.db = db;
        }

        public UserDto CreateAccount(UserDto item)
        {
            
            User user = new User();
            // user.Id = item.Id;
            user.Username = item.Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(item.Password);
            user.FirstName = item.FirstName;
            user.MiddleName = item.MiddleName;
            user.LastName = item.LastName;
            user.BirthDate = item.BirthDate;
            user.Gender = item.Gender;
            user.Phone = item.Phone;
            user.HouseNo = item.HouseNo;
            user.Street = item.Street;
            user.Village = item.Village;
            user.UnitFloor = item.UnitFloor;
            user.Building = item.Building;
            user.ProvinceCode = item.ProvinceCode;
            user.CityCode = item.CityCode;
            user.BrgyCode = item.BrgyCode;
            user.ZipCode = item.ZipCode;
            user.DateCreated =DateTime.UtcNow;
            user.DateUpdated =DateTime.UtcNow;
            user.UserType = item.UserType;
            db.Users.Add(user);
            db.SaveChanges();
            // User Claims
            var claims = new List<Claim>
            {
                new Claim("Username", item.Username.ToString()),
                
            };

            // Encrypt credentials
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var auth = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(8766), // Set to 1 year
                signingCredentials: credentials);

            // Generate JWT
            var token = new JwtSecurityTokenHandler().WriteToken(auth);
            item.Token = token;
            return item;

        }

        public string CreatePersonalInfo(
            IFormFile ValidId, IFormFile SelfieId, string Username, string Password, string FirstName, 
            string MiddleName, string LastName, DateTime BirthDate, string Gender, string Phone, 
            string HouseNo, string Street, string Village, string UnitFloor, string Building, 
            string ProvinceCode, string CityCode, string BrgyCode, string ZipCode, DateTime DateCreated, 
            string UserType
        )
        {
            
            User user = new User();
            // user.Id = item.Id;
            user.Username = Username;
            user.Password = BCrypt.Net.BCrypt.HashPassword(Password);
            user.FirstName = FirstName;
            user.MiddleName = MiddleName;
            user.LastName = LastName;
            user.BirthDate = BirthDate;
            user.Gender = Gender;
            user.Phone = Phone;
            user.HouseNo = HouseNo;
            user.Street = Street;
            user.Village = Village;
            user.UnitFloor = UnitFloor;
            user.Building = Building;
            user.ProvinceCode = ProvinceCode;
            user.CityCode = CityCode;
            user.BrgyCode = BrgyCode;
            user.ZipCode = ZipCode;
            user.DateCreated =DateTime.UtcNow;
            user.DateUpdated =DateTime.UtcNow;
            user.UserType = UserType;
            using (var memoryStream = new MemoryStream())
            {
                ValidId.CopyTo(memoryStream);
                user.ValidId = memoryStream.ToArray();
            }
            using (var memoryStream = new MemoryStream())
            {

                SelfieId.CopyTo(memoryStream);
                user.Selfie = memoryStream.ToArray();
                
            }

            
            db.Users.Add(user);
            db.SaveChanges();
            // User Claims
            var claims = new List<Claim>
            {

                new Claim("Username", Username.ToString()),
                
            };

            // Encrypt credentials
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var auth = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(8766), // Set to 1 year
                signingCredentials: credentials);

            // Generate JWT
            var token = new JwtSecurityTokenHandler().WriteToken(auth);
                
            return "Create Personal Info Created Successfully";

        }

        public string Login(LoginDto loginDto) 
        {

            var user = db.Users.Where(z => z.Username  == loginDto.Username).FirstOrDefault();
            bool verified = false;
            string password = loginDto.Password.Trim();
            if (user != null) 
            {
                verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
                if(verified) 
                {

                    if(loginDto.UserType.Equals("admin")) 
                    {
                        return "Admin Login Success";
                    } 
                    else if(loginDto.UserType.Equals("barangay")) 
                    {

                        return "Barangay Login Success";

                    }
                            
                    else if(loginDto.UserType.Equals("compliant")) 
                    {

                        return "Compliant Login Success";

                    }
                    else
                    {
                        return "Wrong User or Password";
                    }

                }
               
                //Not verified
                else
                {
                    return "Wrong User or Password";
                }
                
            }
            
            return "Wrong User or Password";


        }

    }
}