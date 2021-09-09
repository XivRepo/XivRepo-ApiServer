using System;

namespace XIVRepo.Core.Helpers
{
    public class EnvironmentVariables
    {
        public static string? DatabaseName() => Environment.GetEnvironmentVariable("MYSQL_DATABASE");
        
        public static string? DatabasePassword() => Environment.GetEnvironmentVariable("MYSQL_PASSWORD");
        
        public static string? DatabaseUserId() => Environment.GetEnvironmentVariable("MYSQL_USER");

        public static string? JwtAudience() => Environment.GetEnvironmentVariable("JWT_AUDIENCE");
        
        public static string? JwtIssuer() => Environment.GetEnvironmentVariable("JWT_ISSUER");
        
        public static string JwtKey() => Environment.GetEnvironmentVariable("JWT_KEY") ?? "";

        public static bool InDevMode() => Environment.GetEnvironmentVariable("ENVIRONMENT") == "DEV";
        
        public static int RetryCount() =>
            int.TryParse(Environment.GetEnvironmentVariable("RETRY_COUNT"), out var retryCount) ? retryCount : 0;
    }
}