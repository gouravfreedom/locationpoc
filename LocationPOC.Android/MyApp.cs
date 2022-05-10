using System;
using Android.App;
using Android.Runtime;

namespace LocationPOC.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyCYvJLIo-GG4xkud--HWgVb83xsVZYmwQ8")]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}

