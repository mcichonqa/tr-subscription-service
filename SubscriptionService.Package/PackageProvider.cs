using Newtonsoft.Json;

namespace SubscriptionService.Package
{
    public class PackageProvider
    {
        public List<Package> Packages;

        public PackageProvider()
        {
            string directoryName = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            string content = File.ReadAllText(Path.Combine(directoryName, "Packaages.json"));

            Packages = JsonConvert.DeserializeObject<List<Package>>(content);
        }
    }
}