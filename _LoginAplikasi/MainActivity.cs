using Android.App;
using Android.Widget;
using Android.OS;
using Xamarin.Auth;
using System;

namespace _LoginAplikasi
{
    [Activity(Label = "_LoginAplikasi", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            // SetContentView (Resource.Layout.Main);
            SetContentView(Resource.Layout.Main);
            var facebook = FindViewById<Button>(Resource.Id.fblogin);
            facebook.Click += delegate { LoginToFacebook(true); };

            Button btnsignup = FindViewById<Button>(Resource.Id.btnsignup);
            btnsignup.Click += delegate {
                StartActivity(typeof(SignUpActivity));
            };
        }
        void LoginToFacebook(bool allowCancel)
        {
            var auth = new OAuth2Authenticator(
                clientId: Constants.AppID,
                scope: Constants.Scope,
                authorizeUrl: new Uri(Constants.FacebookRestAPI),
                redirectUrl: new Uri(Constants.LoginSuccessful));

            auth.AllowCancel = allowCancel;
            // If authorization succeeds or is canceled, .Completed will be fired.
            auth.Completed += (s, EventArgs) => {
                if (!EventArgs.IsAuthenticated)
                {
                    var builder = new AlertDialog.Builder(this);
                    builder.SetMessage(Constants.ErrorMessage);
                    builder.SetPositiveButton(Constants.OK, (o, e) => { });
                    builder.Create().Show();
                    return;
                }
                
            };
            
            StartActivity(auth.GetUI(this));
        }

    }
}

