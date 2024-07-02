using Microsoft.AspNetCore.Mvc;

namespace VB.API.Authorization
{
    public class ApiKeyAttribute : ServiceFilterAttribute
    {
        public ApiKeyAttribute() : base(typeof(ApiKeyAuthFilter)) { }
    }
}
