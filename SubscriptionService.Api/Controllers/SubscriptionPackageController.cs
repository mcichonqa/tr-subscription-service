using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Package;
using System;
using System.Linq;

namespace SubscriptionService.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubscriptionPackageController : ControllerBase
    {
        private readonly PackageProvider _packageProvider;

        public SubscriptionPackageController(PackageProvider packageProvider)
        {
            _packageProvider = packageProvider;
        }

        [HttpGet("packages")]
        public IActionResult GetPackages()
        {
            var packages = _packageProvider.Packages;

            if (!packages.Any())
                throw new Exception("Package could not be empty after deserialize.");

            return Ok(packages);
        }
    }
}