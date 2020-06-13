using System;
using Android.App;
using Android.Runtime;

namespace LocationPOC.Droid
{
    [Application]
    [MetaData("com.google.android.maps.v2.API_KEY",
              Value = "AIzaSyCOzvH-CHkEFa4Od7msB5xFaG4vK-UVuzo")]
    public class MyApp : Application
    {
        public MyApp(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }
    }
}

