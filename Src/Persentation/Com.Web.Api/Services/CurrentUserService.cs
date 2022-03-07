using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Com.Application.Common.Interfaces;

namespace Com.Web.Api.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        var user = httpContextAccessor.HttpContext?.User;
        string uId = user?.FindFirstValue(ClaimTypes.Sid);
        UserId = string.IsNullOrEmpty(uId) ? Guid.Empty : Guid.Parse(uId);
        IsAuthenticated = !string.IsNullOrEmpty(uId);
        Role = user?.FindFirstValue(ClaimTypes.Role);
        string fid = user?.FindFirstValue("fid");
        ShopId = string.IsNullOrEmpty(fid) ? Guid.Empty : Guid.Parse(fid);
        string fname = user?.FindFirstValue("fname");
        FacultyName = string.IsNullOrEmpty(fname) ? "-" : fname;
    }

    public Guid UserId { get; }

    public bool IsAuthenticated { get; }

    public string Role { get; }

    public Guid ShopId { get; }

    public string FacultyName { get; }
}
