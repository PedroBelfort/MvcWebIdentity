
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MvcWebIdentity.Services
{
    public class SeedUserClaimsInitial : ISeedUserClaimsInitial
    {
        private readonly UserManager<IdentityUser> userManager;

        public SeedUserClaimsInitial(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task SeedUserClaims()
        {
           try
            {
                IdentityUser user1 = await userManager.FindByEmailAsync("admin@localhost");
                if (user1 is not null)
                {
                    var claimList = (await userManager.GetClaimsAsync(user1)).Select(p => p.Type);

                    if (!claimList.Contains("CadastradoEm"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "09/15/2014"));
                    }

                    if (!claimList.Contains("IsAdmin"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user1, new Claim("IsAdmin", "true"));
                    }
                }

                IdentityUser user2 = await userManager.FindByEmailAsync("usuario@localhost");
                if (user1 is not null)
                {
                    var claimList = (await userManager.GetClaimsAsync(user2)).Select(p => p.Type);

                    if (!claimList.Contains("CadastradoEm"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user1, new Claim("CadastradoEm", "09/15/2020"));
                    }

                    if (!claimList.Contains("IsFuncionario"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user2, new Claim("IsFuncionario", "true"));
                    }

                    if (!claimList.Contains("IsAdmin"))
                    {
                        var claimResult = await userManager.AddClaimAsync(user2, new Claim("IsAdmin", "false"));
                    }
                }


            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
