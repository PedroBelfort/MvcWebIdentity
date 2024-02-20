using Microsoft.AspNetCore.Authorization;

namespace MvcWebIdentity.Policies
{
    public class TempoCadastroRequirement:IAuthorizationRequirement
    {
        public int TempoCadastroMinimo { get; }

        public TempoCadastroRequirement(int tempoCadastroMinimo)
        {
            tempoCadastroMinimo = tempoCadastroMinimo;
        }
    }
}
