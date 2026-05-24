using Microsoft.AspNetCore.Authorization;

namespace Seph.Principal.Infraestructure.Authorization
{
    /*la interfaz IAuthorizationRequirement se utiliza para definir un requisito 
     * de autorización personalizado*/
    public sealed record PermissionRequirement(string Permission): IAuthorizationRequirement;
    
    
}
