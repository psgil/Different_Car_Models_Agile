using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Different_Car_Models_Agile
{
    [Activity(Label = "New_Master_Company")]
    public class New_Master_Company : Activity
    {
        List<Tbl_Company> List_master_company;
        EditText txt_master_company;
        Button btn_save_master_company;
        Button btn_delete_master_company;
        Button btn_update_master_company;
        Button btn_new_master_company;
        Button btn_set_model_rate;

        Spinner spinner;
        TextView txtviewid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Company_Master_Layout );
            // Create your application here


            btn_save_master_company = FindViewById<Button>(Resource.Id.btn_save_master_company);
         
            btn_delete_master_company = FindViewById<Button>(Resource.Id.btn_delete_master_company);

            btn_new_master_company = FindViewById<Button>(Resource.Id.btn_new_master_company);
            btn_update_master_company = FindViewById<Button>(Resource.Id.btn_update_master_company);
            btn_set_model_rate = FindViewById<Button>(Resource.Id.btn_set_model_rate);
            txt_master_company = FindViewById<EditText>(Resource.Id.txt_master_company);
            spinner = FindViewById<Spinner>(Resource.Id.spinner_show);
            txtviewid = FindViewById<TextView>(Resource.Id.txt_v_master_company_id);


            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);

            db.CreateTable<Tbl_Company>();

            load_spiner_master_company();

            spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_ItemSelected);


            btn_save_master_company.Click += btn_save_master_company_Click; ;
            btn_new_master_company.Click += btn_new_master_company_Click;
            btn_delete_master_company.Click += btn_delete_master_company_Click;
            btn_update_master_company.Click += btn_update_master_company_Click;
            btn_set_model_rate.Click += Btn_set_model_rate_Click;

        }

        private void btn_delete_master_company_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);


            var subitem = new Tbl_Models();
            subitem.Cid = Convert.ToInt32(txtviewid.Text);

            var data_s = db.Query<Tbl_Models>("select *  from Tbl_Models where Cid=" + Convert.ToInt32(txtviewid.Text));
            if (data_s.Count > 0)
            {
                Toast.MakeText(this, "Record Will not deleted as Company Exists...,", ToastLength.Short).Show();

            }
            else
            {
                var item = new Tbl_Models();
                item.Id = Convert.ToInt32(txtviewid.Text);
                var data = db.Delete(item);
                Toast.MakeText(this, "Record Deleted Successfully...,", ToastLength.Short).Show();
                txt_master_company.Text = "";
                load_spiner_master_company();

            }

        }

        private void spinner_show_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_master_company.ElementAt(e.Position).Id;
            var masteraccountname = this.List_master_company.ElementAt(e.Position).Company_Name;
            txtviewid.Text = Convert.ToString(id);
            // txt_category.Text = masteraccountname;
            btn_delete_master_company.Enabled = true;
           
        }

        private void load_spiner_master_company()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Tbl_Company>("select *  from Tbl_Company");
            List_master_company = data_s;
            Different_Car_Models_Agile .Resources.Adapter_Company  da = new Resources.Adapter_Company(this, List_master_company);
            spinner.Adapter = da;
           
        }

        private void Btn_set_model_rate_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(New_Model_Rates));
        }

        private void btn_update_master_company_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);

            var item = new Tbl_Company();

            item.Id = Convert.ToInt32(txtviewid.Text);




            item.Company_Name = txt_master_company.Text;


            db.Update(item);

            Toast.MakeText(this, "Record Updated Successfully...,", ToastLength.Short).Show();

            load_spiner_master_company();
        }

        private void btn_new_master_company_Click(object sender, EventArgs e)
        {
            StartActivity(typeof(New_Master_Company));
        }

        private void btn_save_master_company_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Tbl_Company>();
            Tbl_Company tbl = new Tbl_Company();
            tbl.Company_Name  = txt_master_company.Text;
            db.Insert(tbl);
            Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            txt_master_company.Text = "";
            load_spiner_master_company();
        }
    }
}