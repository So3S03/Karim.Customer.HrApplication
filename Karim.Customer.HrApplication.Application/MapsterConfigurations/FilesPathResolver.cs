using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Karim.Customer.HrApplication.Application.MapsterConfigurations
{
    internal class FilesPathResolver(IConfiguration configuration)
    {
        public string Resolve(string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath)) return string.Empty;
            var basePath = configuration["Urls:HRMSBaseUrl"];
            if(string.IsNullOrEmpty(basePath)) return string.Empty;
            return $"{basePath}{relativePath}";
        }
    }
}
