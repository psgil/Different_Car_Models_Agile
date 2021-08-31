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
using SQLite.Net.Attributes;

namespace Different_Car_Models_Agile
{
    [Activity(Label = "New_Model_Rates")]
    public class New_Model_Rates : Activity
    {

        List<Tbl_Models> List_All_Models;
        List<Tbl_Company> List_All_Master_Comapny;
        EditText txtmodelname;
        Button btnsavemodel;
        Button btndeletemodel;
        Button btnupdatemodel;
        Button btnnewmodel;
        ListView ListView1;
        Spinner spinnershowmasterCompany;
        Spinner spinnershowmodel;
        TextView txt_rate;
        TextView txtmodelid;
        TextView txtmastercompanyid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.Set_Model_Rates_Layout) ;
            btnnewmodel = FindViewById<Button>(Resource.Id.btn_new_model);

            btnsavemodel = FindViewById<Button>(Resource.Id.btn_save_model);

            btndeletemodel = FindViewById<Button>(Resource.Id.btn_delete_model);
            btnupdatemodel = FindViewById<Button>(Resource.Id.btn_update_model);
            txtmodelname = FindViewById<EditText>(Resource.Id.txt_model_name);
            spinnershowmasterCompany = FindViewById<Spinner>(Resource.Id.spinner_show_master_company);
            spinnershowmodel = FindViewById<Spinner>(Resource.Id.spinner_show_model);
            txt_rate = FindViewById<TextView>(Resource.Id.txt_Rate);

            txtmodelid = FindViewById<TextView>(Resource.Id.txt_v_model_id);
            txtmastercompanyid = FindViewById<TextView>(Resource.Id.txt_master_company_id);
            ListView1 = FindViewById<ListView>(Resource.Id.listView1);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);

            db.CreateTable<Tbl_Models>();

            load_spiner_Master_Company();
            load_spiner_Models();

            spinnershowmasterCompany.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_Master_Company_ItemSelected);
            spinnershowmodel.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_show_Models_ItemSelected);
            btnsavemodel.Click += Btnsavemodel_Click;
            btndeletemodel.Click += Btndeletemodel_Click;
            btnupdatemodel.Click += Btnupdatemodel_Click;

        }

        private void spinner_show_Master_Company_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_All_Master_Comapny .ElementAt(e.Position).Id;

            txtmastercompanyid.Text = Convert.ToString(id);

            load_spiner_Models();
        }

        private void spinner_show_Models_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinner = (Spinner)sender;
            var id = this.List_All_Models.ElementAt(e.Position).Id;
            var modelname = this.List_All_Models.ElementAt(e.Position).Model_Name;
            var rate = this.List_All_Models.ElementAt(e.Position).Model_Rate;
            txtmodelid.Text = Convert.ToString(id);

            txtmodelname.Text = modelname;
            txt_rate.Text = Convert.ToString(rate);
        }

        private void load_spiner_Master_Company()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Tbl_Company>("select *  from Tbl_Company");
            List_All_Master_Comapny = data_s;
            Different_Car_Models_Agile .Resources.Adapter_Company  da = new Resources.Adapter_Company(this, List_All_Master_Comapny);
            spinnershowmasterCompany.Adapter = da;
        }

        private void load_spiner_Models()
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data_s = db.Query<Tbl_Models>("select *  from Tbl_Models where Cid=" + txtmastercompanyid.Text);
            List_All_Models = data_s;
           Different_Car_Models_Agile .Resources.Adapter_Models  da = new Resources.Adapter_Models(this, List_All_Models);
            spinnershowmodel.Adapter = da;
            ListView1.Adapter = da;
        }

        private void Btnsavemodel_Click(object sender, EventArgs e)
        {
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);
            db.CreateTable<Tbl_Models>();
            Tbl_Models tbl = new Tbl_Models();
            tbl.Model_Name = Convert.ToString(txtmodelname.Text);
            tbl.Cid = Convert.ToInt32(txtmastercompanyid.Text);
            tbl.Model_Rate = Convert.ToInt32(txt_rate.Text);
            db.Insert(tbl);
            Toast.MakeText(this, "Record Added Successfully...,", ToastLength.Short).Show();
            txtmodelname.Text = "";
            load_spiner_Master_Company();
            load_spiner_Models();
        }

        private void Btndeletemodel_Click(object sender, EventArgs e)
        {
            var item = new Tbl_Models();
            item.Id = Convert.ToInt32(txtmodelid.Text);

            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);
            var data = db.Delete(item);
            Toast.MakeText(this, "Record Deleted Successfully...,", ToastLength.Short).Show();
            txtmodelname.Text = "";
            load_spiner_Master_Company();
            load_spiner_Models();
        }

        private void Btnupdatemodel_Click(object sender, EventArgs e)
        {
            var item_model = new Tbl_Models();
            item_model.Id = Convert.ToInt32(txtmodelid.Text);
            item_model.Model_Name  = txtmodelname.Text;
            item_model.Model_Rate = Convert.ToInt32(txt_rate.Text);
            string dpPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "allcars.sqlite");
            var db = new SQLiteConnection(dpPath);

            //db.Update(item_book);

            Toast.MakeText(this, "Record Updated Successfully...,", ToastLength.Short).Show();

            load_spiner_Master_Company();
            load_spiner_Models();
          
        }
    }
}