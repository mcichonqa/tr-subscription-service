using Microsoft.AspNetCore.Mvc;
using SubscriptionService.Contract;
using SubscriptionService.Package;
using System;
using System.Collections.Generic;
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
            List<SubscriptionPackageDto> subPackages = new List<SubscriptionPackageDto>();

            var packages = _packageProvider.Packages;

            if (!packages.Any())
                throw new Exception("Package could not be empty after deserialize.");

            packages.ForEach(package =>
            {
                var subPackage = new SubscriptionPackageDto()
                {
                    Name = package.Name,
                    Duration = DateTime.UtcNow.AddMonths(package.Duration),
                    Details = package.Details,
                    Price = new PriceDto()
                    {
                        GrossValue = package.Price.GrossValue,
                        NetValue = package.Price.NetValue,
                        TotalValue = package.Price.TotalValue
                    }
                };

                subPackages.Add(subPackage);
            });

            return Ok(subPackages);
        }
    }
}