using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace FileSaver.Test
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }
        [Test]
        public void NewTest()
        {
        }

        [Test]
        public void NewTest1()
        {
            app.EnterText(x => x.Class("EntryEditText"), "http://www.informaticaros.com/README.txt");
            app.Tap(x => x.Text("DOWNLOAD"));
        }
    }
}

