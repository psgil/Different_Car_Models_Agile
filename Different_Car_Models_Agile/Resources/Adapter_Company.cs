using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Different_Car_Models_Agile.Resources
{
    public class Adapter_Company : BaseAdapter<Tbl_Company>
    {
        private readonly Activity context;
        private readonly List<Tbl_Company> mItems;
        public Adapter_Company(Activity context, List<Tbl_Company> items)
        {
            this.mItems = items;
            this.context = context;
        }



        public override int Count
        {
            get { return mItems.Count; }

        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override Tbl_Company this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var row = convertView;


            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.List_Compnay_Master , null, false);
            }

            // Set the txtRowName.Text which is in the listview_row layout to the 
            TextView txtRowName = row.FindViewById<TextView>(Resource.Id.txtRowName);
            txtRowName.Text = mItems[position].Company_Name ;



            return row;


        }
    }
}