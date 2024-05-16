using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace OgWeb;

public class AdditionalUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<ApplicationUser, IdentityRole>
{
    public AdditionalUserClaimsPrincipalFactory(
        UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
    {
    }

    public async override Task<ClaimsPrincipal> CreateAsync(ApplicationUser user)
    {
        var principal = await base.CreateAsync(user);
        var identity = (ClaimsIdentity)principal.Identity;

        var claims = new List<Claim>();

        if (user.TwoFactorEnabled)
        {
            claims.Add(new Claim("TwoFactorEnabled", "true"));
        }
        else
        {
            claims.Add(new Claim("TwoFactorEnabled", "false")); ;
        }

        identity.AddClaims(claims);
        return principal;
    }
}
