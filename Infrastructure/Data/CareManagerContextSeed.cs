using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Infrastructure.Data.SeedData;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Data
{
  public  class CareManagerContextSeed
    {
        public static async Task SeedAsync(CareManagerContext context, UserManager<AppUser> userManager, ILoggerFactory loggerFactory)
        {

            try
            {
                if (!userManager.Users.Any())
                {
                    var userData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/users.json");

                    var users = JsonConvert.DeserializeObject<List<UserInfo>>(userData);
                    
                    foreach (var user in users)
                    {
                        AppUser appUser = new AppUser
                        {
                            UserName = user.Email,
                            Email = user.Email,
                            Occupation = user.Occupation,
                            Avatar = user.Avatar,
                            NickName = user.NickName
                        };
                        await userManager.CreateAsync(appUser, "password");
                        

                    }
                    AppUser AdminUser = new AppUser
                    {
                        UserName = "TonyCruz",
                        Email = "tonycruz1@hotmail.com",
                        Occupation = "Developer",
                        Avatar = "https://gravatar.com/avatar/b9f96544793e4769711209438efef470?s=320&r=g&d=https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fps-cdn%2Fdesign-system%2Fassets%2Ftransparent.gif",
                        NickName = "Mr Cruz"
                    };
                    var result = await userManager.CreateAsync(AdminUser, "password");
                    await userManager.AddToRolesAsync(AdminUser, new[] { "Admin", "Dev", "HR", "Client", "Candidate" });
                }
                if (!context.Grades.Any())
                {

                    context.Grades.Add(new Grade { GradeName = "Grade 1" , HourlyRate = 9m});
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 2", HourlyRate = 9.74m });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 3", HourlyRate = 10m });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 4", HourlyRate = 10.76m });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 5", HourlyRate = 11.19m });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 6", HourlyRate = 12.20m });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 7", HourlyRate = 13.50m });
                    await context.SaveChangesAsync();

                }
                if (!context.Candidates.Any())
                {
                    var candidateData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/Candidate.json");

                    var candidates = JsonConvert.DeserializeObject<List<Candidate>>(candidateData);

                    foreach (var item in candidates)
                    {
                        var user = await userManager.FindByEmailAsync(item.Email);
                        item.AppUser = user;
                        item.AppUserId = user.Id;
                        context.Candidates.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Agencies.Any())
                {
                    var agencyData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/Agency.json");

                    var agencies = JsonConvert.DeserializeObject<List<Agency>>(agencyData);

                    foreach (var item in agencies)
                    {
                        var user = await userManager.FindByEmailAsync(item.Email);
                        item.AppUser = user;
                        item.AppUserId = user.Id;
                        context.Agencies.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.PaymentTypes.Any())
                {
                    context.PaymentTypes.Add(new PaymentType { Name = " Daily" });
                    await context.SaveChangesAsync();
                    context.PaymentTypes.Add(new PaymentType { Name = " Hourly" });
                    await context.SaveChangesAsync();
                }
                if (!context.AttributeDetails.Any())
                {
                    var att1 = new AttributeDetail { AttributeName = "Male" };
                    var att2 = new AttributeDetail { AttributeName = "Female" };
                    var att3 = new AttributeDetail { AttributeName = "Male or Female" };

                    context.AttributeDetails.Add(att1);
                    await context.SaveChangesAsync();
                    context.AttributeDetails.Add(att2);
                    await context.SaveChangesAsync();
                    context.AttributeDetails.Add(att3);
                    await context.SaveChangesAsync();
                }
                if (!context.JobTypes.Any())
                {

                    context.JobTypes.Add(new JobType { JobName = "Care worker" });
                    await context.SaveChangesAsync();
                    context.JobTypes.Add(new JobType { JobName = "Nouse" });
                    await context.SaveChangesAsync();
                    context.JobTypes.Add(new JobType { JobName = "Healthcare Assistants" });
                    await context.SaveChangesAsync();
                    context.JobTypes.Add(new JobType { JobName = "Assistant practitioner" });

                    await context.SaveChangesAsync();
                }
                if (!context.ShiftStates.Any())
                {

                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Pending" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "In Progress" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Booked" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Finish" });
                    await context.SaveChangesAsync();

                }
                if (!context.Arias.Any())
                {
                    var ariaData =
                       File.ReadAllText("../Infrastructure/Data/SeedData/Arias.json");
                    var ariasInfo = JsonConvert.DeserializeObject<List<AriaInfo>>(ariaData);
                    foreach (var item in ariasInfo)
                    {
                        var newAriad = new Aria { Name = item.Name, Borough = item.Borough };
                        context.Arias.Add(newAriad);
                    }

                    await context.SaveChangesAsync();

                }
                if (!context.TimeDetails.Any())
                {
                    var timeData =
                       File.ReadAllText("../Infrastructure/Data/SeedData/timeDetails.json");
                    var timeDetailInfos = JsonConvert.DeserializeObject<List<TimeDetailInfo>>(timeData);
                    foreach (var item in timeDetailInfos.OrderBy(tdi => tdi.Id))
                    {
                        var td = new TimeDetail { Hour = item.Hour };
                        context.TimeDetails.Add(td);
                        await context.SaveChangesAsync();
                    }



                }
                if (!context.ClientLocations.Any())
                {
                    var clientLocationData =
                       File.ReadAllText("../Infrastructure/Data/SeedData/ClientLocation.json");
                    var clientInfo = JsonConvert.DeserializeObject<List<ClientLocationInfo>>(clientLocationData);
                    foreach (var item in clientInfo)
                    {
                        var cl = new ClientLocation
                        {
                            CompanyName = item.CompanyName,
                            ManagerName = item.ManagerName,
                            ContactName = item.ContactName,
                            Address1 = item.Address1,
                            Address2 = item.Address2,
                            Address3 = item.Address3,
                            Address4 = item.Address4,
                            Address5 = item.Address5,
                            ContactNumber = item.ContactNumber,
                            AgencyId = item.AgencyId
                        };
                        context.ClientLocations.Add(cl);
                    }

                    await context.SaveChangesAsync();

                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<CareManagerContext>();
                logger.LogError(ex.Message);
            }
        }
        public static async Task SeedAsync(CareManagerContext context, UserManager<AppUser> userManager, ILoggerFactory loggerFactory, RoleManager<IdentityRole> roleManager)
        {

			try
			{
                if (!userManager.Users.Any())
                {
                    var userData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/users.json");

                    var users = JsonConvert.DeserializeObject<List<UserInfo>>(userData);
                    var roles = new List<IdentityRole>
                    {
                        new IdentityRole {Name = "Candidate" },
                        new IdentityRole {Name = "Client" },
                        new IdentityRole {Name = "HR" },
                        new IdentityRole {Name = "Admin" },
                        new IdentityRole {Name = "Dev" },
                    };
                    foreach (var role in roles)
                    {
                        await roleManager.CreateAsync(role);
                    }
                    foreach (var user in users)
                    {
                        AppUser appUser = new AppUser
                        {
                            UserName = user.Email,
                            Email = user.Email,
                            Occupation = user.Occupation,
                            Avatar = user.Avatar,
                            NickName = user.NickName
                        };
                       await userManager.CreateAsync(appUser, "password");
                       await userManager.AddToRoleAsync(appUser, user.Occupation);

                    }
                    AppUser AdminUser = new AppUser
                    {
                        UserName = "TonyCruz",
                        Email = "tonycruz1@hotmail.com",
                        Occupation = "Developer",
                        Avatar = "https://gravatar.com/avatar/b9f96544793e4769711209438efef470?s=320&r=g&d=https%3A%2F%2Fs3-us-west-2.amazonaws.com%2Fps-cdn%2Fdesign-system%2Fassets%2Ftransparent.gif",
                        NickName = "Mr Cruz"
                    };
                   var result = await userManager.CreateAsync(AdminUser, "password");
                    await userManager.AddToRolesAsync(AdminUser, new[] { "Admin", "Dev", "HR", "Client", "Candidate" });
                }
                if (!context.Grades.Any())
                {

                    context.Grades.Add(new Grade { GradeName = "Grade 1" });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 2" });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 3" });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 4" });
                    await context.SaveChangesAsync();
                    context.Grades.Add(new Grade { GradeName = "Grade 5" });
                    await context.SaveChangesAsync();

                }
                    if (!context.Candidates.Any())
                {
                    var candidateData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/Candidate.json");

                    var candidates = JsonConvert.DeserializeObject< List <Candidate>>(candidateData);

                    foreach (var item in candidates)
                    {
                        var user = await userManager.FindByEmailAsync(item.Email);
                        item.AppUser = user;
                        item.AppUserId = user.Id;
                        context.Candidates.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.Agencies.Any())
                {
                    var agencyData =
                        File.ReadAllText("../Infrastructure/Data/SeedData/Agency.json");

                    var agencies = JsonConvert.DeserializeObject<List<Agency>>(agencyData);

                    foreach (var item in agencies)
                    {
                        var user = await userManager.FindByEmailAsync(item.Email);
                        item.AppUser = user;
                        item.AppUserId = user.Id;
                        context.Agencies.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.PaymentTypes.Any())
                {
                    context.PaymentTypes.Add(new PaymentType { Name = " Daily" });
                    await context.SaveChangesAsync();
                    context.PaymentTypes.Add(new PaymentType { Name = " Hourly" });
                    await context.SaveChangesAsync();
                }
                if (!context.AttributeDetails.Any())
                {
                    var att1 = new AttributeDetail { AttributeName = "Male" };
                    var att2 = new AttributeDetail { AttributeName = "Female" };
                    var att3 = new AttributeDetail { AttributeName = "Male or Female" };
                    
                    context.AttributeDetails.Add(att1);
                    await context.SaveChangesAsync();
                    context.AttributeDetails.Add(att2);
                    await context.SaveChangesAsync();
                    context.AttributeDetails.Add(att3);
                    await context.SaveChangesAsync();
                }
                if (!context.JobTypes.Any())
                {

                    context.JobTypes.Add(new JobType { JobName = "Care worker" });
                    await context.SaveChangesAsync();
                    context.JobTypes.Add(new JobType { JobName = "Nouse" });
                    await context.SaveChangesAsync();
                    context.JobTypes.Add(new JobType { JobName = "Healthcare Assistants" });
                    await context.SaveChangesAsync();
                    context.JobTypes.Add(new JobType { JobName = "Assistant practitioner" });

                    await context.SaveChangesAsync();
                }
                if (!context.ShiftStates.Any())
                {

                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Pending" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "In Progress" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Booked" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Finish" });
                    await context.SaveChangesAsync();
                    context.ShiftStates.Add(new ShiftState { ShiftDetails = "Canceled" });
                    await context.SaveChangesAsync();

                }
                if (!context.Arias.Any())
                {
                    var ariaData =
                       File.ReadAllText("../Infrastructure/Data/SeedData/Arias.json");
                    var ariasInfo = JsonConvert.DeserializeObject<List<AriaInfo>>(ariaData);
                    foreach (var item in ariasInfo)
                    {
                        var newAriad = new Aria { Name = item.Name, Borough = item.Borough };
                        context.Arias.Add(newAriad);
                    }

                    await context.SaveChangesAsync();

                }
                if (!context.TimeDetails.Any())
                {
                    var timeData =
                       File.ReadAllText("../Infrastructure/Data/SeedData/timeDetails.json");
                    var timeDetailInfos = JsonConvert.DeserializeObject<List<TimeDetailInfo>>(timeData);
                    foreach (var item in timeDetailInfos.OrderBy(tdi => tdi.Id))
                    {
                        var td = new TimeDetail { Hour = item.Hour};
                        context.TimeDetails.Add(td);
                        await context.SaveChangesAsync();
                    }

                    

                }
                if (!context.ClientLocations.Any())
                {
                    var clientLocationData =
                       File.ReadAllText("../Infrastructure/Data/SeedData/ClientLocation.json");
                    var clientInfo = JsonConvert.DeserializeObject<List<ClientLocationInfo>>(clientLocationData);
                    foreach (var item in clientInfo)
                    {
                        var cl = new ClientLocation
                        {
                            CompanyName = item.CompanyName,
                            ManagerName = item.ManagerName,
                            ContactName = item.ContactName,
                            Address1 = item.Address1,
                            Address2 = item.Address2,
                            Address3 = item.Address3,
                            Address4 = item.Address4,
                            Address5 = item.Address5,
                            ContactNumber = item.ContactNumber,
                            AgencyId = item.AgencyId
                        };
                        context.ClientLocations.Add(cl);
                    }

                    await context.SaveChangesAsync();

                }


            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<CareManagerContext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
