namespace Envisia.Infrastructure.Authorization
{
    public static class AuthorizationExtensions
    {
        public static bool ValidPermission(this string permission, Permission permissionEnum)
        {
            if (Enum.TryParse<Permission>(permission, out var parsedEnum))
            {
                return permissionEnum == parsedEnum;
            }

            return false;
        }
    }
}
