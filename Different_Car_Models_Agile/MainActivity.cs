using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using SQLite;

namespace Different_Car_Models_Agile
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        EditText txtusername;
        EditText txtPassword;
        Button btncreate;
        Button btnsign;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            btnsign = FindViewById<Button>(Resource.Id.btnlogin);
            btncreate = FindViewById<Button>(Resource.Id.btnregister);
            txtusername = FindViewById<EditText>(Resource.Id.txtusername);
            txtPassword = FindViewById<EditText>(Resource.Id.txtpwd);

            btnsign.Click += Btnsign_Click;
            btncreate.Click += Btncreate_Click;

        }



        private void Btncreate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(New_Register_User_Activity));
        }

        private void Btnsign_Click(object sender, EventArgs e)
        {
            try
            {
                string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
                var db = new SQLiteConnection(dpPath);
                var data = db.Table<LoginTable>();

                var data1 = data.Where(x => x.username == txtusername.Text && x.password == txtPassword.Text).FirstOrDefault();

                if (data1 != null)
                {
                    Toast.MakeText(this, "Login Success", ToastLength.Short).Show();

                    StartActivity(typeof(New_Master_Company));

                }
                else
                {
                    Toast.MakeText(this, "Username or Password invalid", ToastLength.Short).Show();
                }
            }
            catch (Exception ex)
            {
                Toast.MakeText(this, ex.ToString(), ToastLength.Short).Show();

            }

        }
    }
}

