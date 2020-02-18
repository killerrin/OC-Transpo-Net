using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace OCTranspo_Net.Test
{
    [TestClass]
    public class OCTranspoServiceTests
    {
        static IConfigurationRoot Configuration;
        static OCTranspoService TranspoService;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                //.AddJsonFile("appsettings.json", optional:false, reloadOnChange:true)
                .AddUserSecrets("4296832b-6b0f-461f-853c-75da4c60a0e2");
            Configuration = builder.Build();
            TranspoService = new OCTranspoService(Configuration["appID"], Configuration["apiKey"]);
        }

        [TestMethod]
        public void GetRouteSummaryForStop_Test()
        {
            var result = TranspoService.GetRouteSummaryForStop("3037", "97").Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetNextTripsForStop_Test()
        {
            var result = TranspoService.GetNextTripsForStop("3037", "97").Result;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetNextTripsForStopAllRoutes_Test()
        {
            var result = TranspoService.GetNextTripsForStopAllRoutes("7659").Result;
            Assert.IsNotNull(result);
        }
    }
}
