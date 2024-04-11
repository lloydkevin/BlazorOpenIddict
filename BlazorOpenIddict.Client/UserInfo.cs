using System.Security.Claims;

namespace BlazorOpenIddict.Client;

// damienbod/Blazor.BFF.OpenIDConnect.Template/BlazorBffOpenIdConnect/Shared/Authorization/UserInfo.cs
// dotnet/blazor-samples/8.0/BlazorWebAppOidcBff/BlazorWebAppOidc.Client/UserInfo.cs

// Add properties to this class and update the server and client AuthenticationStateProviders
// to expose more information about the authenticated user to the client.
public class UserInfo
{
public required string UserId { get; init; }
    public required string Name { get; init; }

    public const string UserIdClaimType = "sub";
    public const string NameClaimType = "name";

    public ICollection<ClaimValue> Claims { get; set; } = [];

    public static UserInfo FromClaimsPrincipal(ClaimsPrincipal principal)
    {
        UserInfo userInfo = new()
        {
            UserId = GetRequiredClaim(principal, UserIdClaimType),
            Name = GetRequiredClaim(principal, NameClaimType),
        };
        var claims = principal.Claims.Select(u => new ClaimValue(u.Type, u.Value))
            .ToList();

        userInfo.Claims = claims;

        return userInfo;
    }

    public ClaimsPrincipal ToClaimsPrincipal()
    {
        var claims = Claims.Select(u => new Claim(u.Type, u.Value))
            .ToList();

        ClaimsPrincipal principal = new(new ClaimsIdentity(
            claims,
            authenticationType: nameof(UserInfo),
            nameType: NameClaimType,
            roleType: null));
        return principal;
    }

    private static string GetRequiredClaim(ClaimsPrincipal principal, string claimType) =>
        principal.FindFirst(claimType)?.Value ?? throw new InvalidOperationException($"Could not find required '{claimType}' claim.");
}
