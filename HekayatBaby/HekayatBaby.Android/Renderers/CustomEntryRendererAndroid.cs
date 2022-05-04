using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using HekayatBaby.Controls;
using HekayatBaby.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRendererAndroid))]
namespace HekayatBaby.Droid.Renderers
{
    class CustomEntryRendererAndroid : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                var gradiantDrawable = new GradientDrawable();
                gradiantDrawable.SetStroke(1, Android.Graphics.Color.WhiteSmoke);
                gradiantDrawable.SetColor(Android.Graphics.Color.WhiteSmoke);
                gradiantDrawable.SetCornerRadius(20);

                Control.SetBackground(gradiantDrawable);

                //       Control.SetShadowLayer(5, 50, 50, Android.Graphics.Color.Red);

            }

        }

    }
}