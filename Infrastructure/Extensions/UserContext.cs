using Application.Constants;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastructure.Extensions
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return GlobalConstants.GuestUser;
            }
            return userId;
        }
    }
}
