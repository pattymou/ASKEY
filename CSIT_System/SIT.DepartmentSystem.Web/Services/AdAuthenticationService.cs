using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SIT.DepartmentSystem.Web.Data;
using SIT.DepartmentSystem.Web.Models;
using System.DirectoryServices.AccountManagement;
using System.Net.Mime;
using System.Security.Claims;

namespace SIT.DepartmentSystem.Web.Services;

public class AdAuthenticationService(IConfiguration configuration, AppDbContext db)
{
    public async Task<ClaimsPrincipal?> AuthenticateAsync(string username, string password)
    {
        var domain = configuration["Ad:Domain"];
        var container = configuration["Ad:Container"];
        var ok = false;

        try
        {
            using var context = string.IsNullOrWhiteSpace(container)
                ? new PrincipalContext(ContextType.Domain, domain)
                : new PrincipalContext(ContextType.Domain, domain, container);

            ok = context.ValidateCredentials(username, password);
        }
        catch
        {
            // 開發測試備援帳號
            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin")
            {
                ok = true;
            }
        }

        if (!ok) return null;

        var account = username.ToLowerInvariant();
        var user = await db.Users.FirstOrDefaultAsync(x => x.Account == account);

        if (user is null)
        {
            user = new AppUser
            {
                Account = account,
                DisplayName = username,
                Department = "DA40",
                Email = $"{account}@example.com",
                IsAdmin = account == "admin"
            };

            db.Users.Add(user);
            await db.SaveChangesAsync();
        }

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Account),
            new(ClaimTypes.Name, user.DisplayName),
            new(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
            new("account", user.Account),
            new("department", user.Department)
        };

        return new ClaimsPrincipal(
            new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme)
        );
    }
}