using Auth0.OidcClient;

namespace iOSSample;

[Register ("AppDelegate")]
public class AppDelegate : UIApplicationDelegate {

    MyViewController myViewController = new MyViewController();

    public override UIWindow? Window {
		get;
		set;
	}

    public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
    {
        ActivityMediator.Instance.Send(url.AbsoluteString);

        return true;
    }

    public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
	{
		// create a new window instance based on the screen size
		Window = new UIWindow (UIScreen.MainScreen.Bounds);

		Window.RootViewController = myViewController;

		// make the window visible
		Window.MakeKeyAndVisible ();

		return true;
	}
}

