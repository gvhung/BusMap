using NUnit.Framework;
using Xamarin.UITest;

namespace BusMap.Mobile.UITests
{
    public abstract class TestAbstractClass
    {
        protected IApp app;
        protected Platform platform;

        public TestAbstractClass(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }
    }
}
