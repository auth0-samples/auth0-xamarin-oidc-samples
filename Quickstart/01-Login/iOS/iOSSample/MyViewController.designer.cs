// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System.CodeDom.Compiler;

namespace iOSSample;

[Register ("MyViewController")]
partial class MyViewController
{
    [Outlet]
    [GeneratedCode("iOS Designer", "1.0")]
    UIKit.UIButton LoginButton { get; set; }

    [Outlet]
    [GeneratedCode("iOS Designer", "1.0")]
    UIKit.UITextView UserDetailsTextView { get; set; }

    void ReleaseDesignerOutlets()
    {
        if (LoginButton != null)
        {
            LoginButton.Dispose();
            LoginButton = null;
        }

        if (UserDetailsTextView != null)
        {
            UserDetailsTextView.Dispose();
            UserDetailsTextView = null;
        }
    }
}

