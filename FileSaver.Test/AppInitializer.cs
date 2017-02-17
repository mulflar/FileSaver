using Xamarin.UITest;

namespace FileSaver.Test
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .DeviceSerial("LGH340nf9b19ac8")
                    .InstalledApp("com.mulflar.filesaver")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .StartApp();
        }
    }
}

