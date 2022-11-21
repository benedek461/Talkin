using ChatAPI.Services.Interfaces;

namespace ChatAPI.Services
{
    public class HttpContextService : IHttpContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private int? _userId;
        public int UserId
        {
            get
            {
                if (_userId == null)
                {
                    _userId = GetUserId();
                }

                return (int)_userId;
            }
        }

        private int GetUserId()
        {
            return Convert.ToInt32(
                _httpContextAccessor.HttpContext.User.Claims
                .ToList()
                .FirstOrDefault(x => x.Type == "Id").Value);
        }
    }
}
