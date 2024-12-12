using System.Security.Claims;
using BlazorCalendar.Shared;
using BlazorCalendar.Shared.DTOs.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace BlazorCalendar.Client.Common;

public class PersistingAuthenticationStateProvider : AuthenticationStateProvider
{
    private static readonly Task<AuthenticationState> _default =
        Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

    private readonly Task<AuthenticationState> _authenticationStateTask = _default;

    public PersistingAuthenticationStateProvider(PersistentComponentState persistentComponentState)
    {
        if (!persistentComponentState.TryTakeFromJson<UserInfo>(Constants.UserInfoState, out var userInfo) ||
            userInfo is null) return;

        var claims = new List<Claim>
        {
            new(Constants.CustomClaims.UserId, userInfo.UserId),
            new(ClaimTypes.Name, userInfo.UserName)
        };

        var identity = new ClaimsIdentity(claims, "Cookies");
        var principal = new ClaimsPrincipal(identity);

        _authenticationStateTask = Task.FromResult(new AuthenticationState(principal));
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        return _authenticationStateTask;
    }
}