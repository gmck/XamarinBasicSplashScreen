using Android.App;
using Android.OS;
using AndroidX.AppCompat.App;

namespace com.companyname.XamarinBasicSplashScreen
{
    [Activity(Label = "@string/app_name", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);

            base.OnCreate(savedInstanceState);

            // If you want to get a good look at the icon to check it. Don't forget remove from production code
            //System.Threading.Thread.Sleep(1000);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
        }
        
    }
}