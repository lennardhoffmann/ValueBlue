namespace VB.API.Logic
{
    public static class AuthorizationService
    {

        public static void ValidateAuthenticationNameParameter(string authUserName, string configuredAdminUserName)
        {
            if (!string.Equals(authUserName.ToLower(), configuredAdminUserName.ToLower()))
            {
                throw new BadHttpRequestException("Invalid auth username provided");
            }
        }
    }
}
