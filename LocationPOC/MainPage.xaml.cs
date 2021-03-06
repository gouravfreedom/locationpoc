﻿using System;
using System.Collections.Generic;
using System.Linq;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace LocationPOC
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            //var response = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.LocationWhenInUse);
            if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                {
                    //await DisplayAlert("Need location", "Gunna need that location", "OK");
                }

                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.LocationWhenInUse);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.LocationWhenInUse))
                    status = results[Permission.LocationWhenInUse];
            }

            if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
            {

                map.MapType = MapType.Satellite;
                var location = await Geolocation.GetLocationAsync();

                //var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    Pin currentPin = new Pin()
                    {
                        Type = PinType.Generic,
                        Label = "Current",
                        Address = "Current Location",
                        Position = new Position(location.Latitude,location.Longitude),
                        //Rotation = 33.3f,
                        Tag = "id_tokyo"
                    };

                    map.Pins.Add(currentPin);
                    // Add Circles
                    var circle1 = new Circle();
                    circle1.StrokeWidth = 2f;
                    circle1.StrokeColor = Color.Red;
                    circle1.FillColor = Color.Transparent;
                    circle1.Center = new Position(location.Latitude, location.Longitude);
                    circle1.Radius = Distance.FromMeters(50);
                    map.Circles.Add(circle1);
                    //map.Circles.Add(CreateShiftedCircle(circle1, 0d, 0.05d, Color.Yellow));
                    //map.Circles.Add(CreateShiftedCircle(circle1, 0d, 0.10d, Color.Green));
                    map.MoveToRegion(MapSpan.FromCenterAndRadius(
                       new Position(location.Latitude, location.Longitude), Distance.FromMiles(0.3)));

                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }
            }
            else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
            {

            }


            try
            {

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

        }
    }
}