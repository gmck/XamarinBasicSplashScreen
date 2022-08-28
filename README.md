XamarinSimpleSplashScreen

Most of the NavigationGraph tutorial projects here use the new Splash API using Xamarin.AndroidX.Core.SplashScreen. However, after going back and forward with another Xamarin Forms developer who basically wasn’t interested in the NavigationComponent he added the following comment at the end of our exchange.

I think what would have been helpful at the beginning if your sample project did not contain all that extra good stuff but just the splash screen, I would have stripped out all the rest, as it would have been a tad easier to follow especially if you mainly do forms and don't really touch android much.

So this XamarinSimpleSplashScreen project is just that – simple. It just does the basic SplashScreen.

You should be able to build your own version in just a couple of minutes. Simply create a new project using the Blank app template, you only have to add a single line of code to what is already there to make it work with all the devices you deploy to.
As with all my projects, I take a few extra steps after the project is loaded in Visual Studio. Please follow the following simple steps.
1. Open Visual Studio 2022 - Create a new project
2. From the templates choose Android App (Xamarin) 
3. Give the project a name
4. Select a template for your app – choose Blank app
5. To be on the safe side increase the minimum Android version to 7.0 (more on that later)

The project is now created and you should be looking at the MainActivity.cs within Visual Studio. Don’t build the app just yet, we have some housekeeping to attend to first. 

1. Strip out all the Xamarin.Essentials code we don’t need it and therefore remove the Xamarin.Essentials NuGet. At the same time while you are using the NuGet Package manager go to updates and select all to update the two remaining Nugets packages Xamarin.AndroidX.AppCompat and Xamarin.Google.Android.Material to 1.4.2.1 and 1.6.1.1 respectively.
2. Now the housekeeping. Close the Package manager and Open the properties window of the app. Locate the Default NameSpace: and add in front of whatever is there “com.companyname.” without the quotes and copy “com.companyname.” without the quotes to the clipboard. Ensure that Compile Using Android version (Target Framework) is set to Android 12. 

Close the properties window and click on the folder Resources in the Solution Explorer. Open the file Resource. designer.cs. We want to change the first line, the global  Android.Runtime.ResourceDesignerAttribute and the namespace, by pasting “com.companyname.” at beginning of the value and at the beginning of the namespace. If you are not sure of this step check it in the provided project. We also need to update the namespace of the MainActivity in the same way.

3. You are now ready to build and deploy the project. If you are deploying to an Android 12 device you are already done as Android 12 will automatically display the Xamarin Application Icon as the app starts. However, if you are deploying to a device less than Android 12 we still have work to do as those devices will just show a blank screen at startup.

Note that as yet we haven’t even added SplashScreen Api to our app, which proves that an app compiled with Android 12 and targeting a device running Android 12 or above does not need to do anything more, as obviously, the Android runtime is handling the Splash screen automatically.

Now we need to get devices running less than Android 12 working with a splash screen showing the Xamarin Application Icon.

1. Open the Nuget package manager and search for androidx.core.splash and make sure include pre-release is checked. This is just a coincidence, but as I’m writing this, release 1.0.0 is now available, so you no longer need to check the prerelease checkbox.
2. Add the following line of code before base.OnCreate() in the MainActivity.
AndroidX.Core.SplashScreen.SplashScreen.InstallSplashScreen(this);
3. Add the following style to styles.xml
<style name="AppTheme.Starting" parent="Theme.SplashScreen.IconBackground">
		<item name="windowSplashScreenAnimatedIcon">@mipmap/ic_launcher_foreground</item>
		<item name="windowSplashScreenIconBackgroundColor">@color/ic_launcher_background</item>
		<item name="windowSplashScreenBackground">?android:colorBackground</item>
		<item name="postSplashScreenTheme">@style/AppTheme</item>
	</style>
4. Modify the Activity attribute of the MainActivity by removing Theme = "@style/AppTheme". This is automatically handled by the item postSplashScreenTheme. 
5. Finally open the project’s Properties and under the manifest tab change the Application Theme from @style/AppTheme to @style/AppTheme.Starting

Rebuild and deploy and now all devices between Android 7.0 and Android 13 will display the Xamarin Application Icon as a splash screen before the MainActivity is rendered.

Now swap to the Release build and change the following via the tab Android Options Code Shrinker r8, enable startup tracing, and set Linking to SDK and User Assemblies. Rebuild and deploy.

This will result in the fastest possible start on any device and the smallest apk. You can measure the result by searching for the phrase “Displayed” in the log cat logs. Remember to clear the app from most recents every time you launch it as the splash screen only displays when doing a cold start. As an example, this shows a value of 246ms on a Pixel 6 running Android 13. If you want to artificially slow it down to better view the icon add System.Threading.Thread.Sleep(1000) before SetContentView(). Just don’t forget to comment that line for production code when you are done.

All that is needed now is to swap out your application icons for the Xamarin icons.

Note: Earlier, I specifically stated devices Android 7.0 to Android 13. I only have specific devices I can deploy to within that range. I believe that the SplashApi is applicable down to Android 5.0 or API 21. However, as I no longer have working devices below Android 7 API 24 I can’t confirm that it works with devices less than Android 7 API 24.  

To view the discussion re other features on the SplashApi please see the following discussion https://github.com/xamarin/xamarin-android/issues/7239

Visual Studio 2022 version used 
VS 2022 17.4.0 Preview 1.0 - 28 August 2022
