using System;
using System.Diagnostics;
using System.Text;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using CxxiGeneratedNamespace.ClippingLibrary;


namespace TestMonoDroid
{


    [Activity (Label = "TestMonoDroid", MainLauncher = true)]
    public class ActivityMain : Activity
    {
        private static string RunTest()
        {
            var w = new Wrapper();

            w.InitializeSubject();
            w.AddOuterRing();
            w.AddPoint(0, 0);
            w.AddPoint(0, 1000);
            w.AddPoint(1000, 1000);
            w.AddPoint(1000, 0);
            w.AddPoint(0, 0);
            w.AddInnerRing();
            w.AddPoint(100, 100);
            w.AddPoint(900, 100);
            w.AddPoint(900, 200);
            w.AddPoint(100, 200);
            w.AddPoint(100, 100);
            w.AddInnerRing();
            w.AddPoint(100, 700);
            w.AddPoint(900, 700);
            w.AddPoint(900, 900);
            w.AddPoint(100, 900);
            w.AddPoint(100, 700);

            w.InitializeClip();
            w.AddOuterRing();
            w.AddPoint(-20, 400);
            w.AddPoint(-20, 600);
            w.AddPoint(1020, 600);
            w.AddPoint(1020, 400);
            w.AddPoint(-20, 400);
            w.AddInnerRing();
            w.AddPoint(100, 450);
            w.AddPoint(900, 450);
            w.AddPoint(900, 550);
            w.AddPoint(100, 550);
            w.AddPoint(100, 450);

            w.ExecuteDifference();

            return GetOutputString(w);
        }


        private static string GetOutputString(Wrapper w)
        {
            var output = new StringBuilder();

            while (w.GetOuterRing())
            {
                output.Append("( /n");

                output.Append(" ( /n");

                double x = 0;
                double y = 0;
                while (w.GetPoint(ref x, ref y))
                {
                    output.AppendFormat("   ({0};{1}) /n", x, y);
                }

                output.Append(" ) /n");

                while (w.GetInnerRing())
                {
                    output.Append(" ( /n");

                    while (w.GetPoint(ref x, ref y))
                    {
                        output.AppendFormat("   ({0};{1}) /n", x, y);
                    }

                    output.Append(" ) /n");
                }

                output.Append(") /n");
            }

            output.Replace("/n", System.Environment.NewLine);
            return output.ToString();
        }


        protected override void OnCreate (Bundle bundle)
        {
            base.OnCreate (bundle);

            SetContentView (Resource.Layout.Main);

            var btn = FindViewById<Button>(Resource.Id.btnRunTest);
            btn.Click +=
                (sender, eButton) =>
                {
                    var str = RunTest();
                    var txt = FindViewById<TextView>(Resource.Id.txtResult);
                    txt.Text = str;
                };
        }
    }


}