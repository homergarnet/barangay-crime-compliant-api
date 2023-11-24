# barangay-crime-compliant-api
---------------------------------------------------------------------------------------------------------------
How to publish dotnet core using CLI
dotnet publish -c release
---------------------------------------------------------------------------------------------------------------
for dotnet models
//FOR windows
dotnet ef dbcontext scaffold "Server=DESKTOP-JA43BLA;Database=Thesis_Crime;Trusted_Connection=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force

//FOR macbook IOS
dotnet ef dbcontext scaffold "Server=localhost;Database=Thesis;User ID=sa;Password=Password!1234; TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o Models --force
---------------------------------------------------------------------------------------------------------------
to access swagger
http://localhost:8001/swagger/index.html
----------------------------------------------------------------------------------------------------------------------------------------
types of account:
1.)admin
2.)barangay
3.)compliant
4.)police
---------------------------------------------------------------------------------------------------------------
Type of Statuses:

1.) active
2.) fake report
3.) resolved
4.) in progress
5.) investigation
6.) completed
7.) closed
8.) pending
---------------------------------------------------------------------------------------------------------------
Type of Recidency:
1.) permanent
2.) short term