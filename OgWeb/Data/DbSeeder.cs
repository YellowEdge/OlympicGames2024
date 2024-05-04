using Microsoft.AspNetCore.Routing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using OgWeb.Constants;

namespace OgWeb.Data;

public class DbSeeder
{
    public static async Task SeedDefaultData(IServiceProvider service)
    {
        var userMgr = service.GetService<UserManager<ApplicationUser>>();
        var roleMgr = service.GetService<RoleManager<IdentityRole>>();
        //adding some roles to db
        var adminRoleExits = await roleMgr.FindByNameAsync(Roles.admin.ToString());
        var clientRoleExits = await roleMgr.FindByNameAsync(Roles.client.ToString());

        if (adminRoleExits is null)
        {
            await roleMgr.CreateAsync(new IdentityRole(Roles.admin.ToString()));
            //adminRoleExits = await roleMgr.FindByNameAsync(Roles.admin.ToString());
            //if (!adminRoleExits.NormalizedName.Equals(Roles.admin.ToString()))
            //{
            //    adminRoleExits.NormalizedName = "admin";
            //}
        }

        if (clientRoleExits is null)
        {
            await roleMgr.CreateAsync(new IdentityRole(Roles.client.ToString()));
            //clientRoleExits = await roleMgr.FindByNameAsync(Roles.client.ToString());
            //if (!clientRoleExits.NormalizedName.Equals(Roles.client.ToString()))
            //{
            //    clientRoleExits.NormalizedName = "client";
            //}
        }

        // create admin user
        var adminUserExists = userMgr.FindByEmailAsync("admin@paris.jo");
        if(adminUserExists is null)
        {
            var admin = new ApplicationUser
            {
                UserName = "admin@paris.jo",
                Email = "admin@paris.jo",
                EmailConfirmed = true
            };

            var adminInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (adminInDb is null)
            {
                await userMgr.CreateAsync(admin, "Azerty01$");
                await userMgr.AddToRoleAsync(admin, Roles.admin.ToString());
            }
        }

        //create client user
        var testUserExists = userMgr.FindByEmailAsync("ana@paris.jo");
        if(testUserExists is null)
        {
            var ana = new ApplicationUser
            {
                UserName = "ana@paris.jo",
                Email = "ana@paris.jo",
                EmailConfirmed = true
            };

            var clientInDb = await userMgr.FindByEmailAsync(ana.Email);
            if (clientInDb is null)
            {
                await userMgr.CreateAsync(ana, "Azerty01$");
                await userMgr.AddToRoleAsync(ana, Roles.client.ToString());
            }
        }
    }
}