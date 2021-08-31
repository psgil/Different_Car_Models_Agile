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
   public  class Adapter_Models : BaseAdapter<Tbl_Models>
    {
        private readonly Activity context;
        private readonly List<Tbl_Models> mItems;

        public Adapter_Models(Activity context, List<Tbl_Models> items)
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

        public override Tbl_Models this[int position]
        {
            get { return mItems[position]; }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {

            var row = convertView;


            if (row == null)
            {
                row = LayoutInflater.From(context).Inflate(Resource.Layout.List_Model_Rates , null, false);
            }







            TextView txtRowName_food = row.FindViewById<TextView>(Resource.Id.txtRowName_model);
            txtRowName_food.Text = mItems[position].Model_Name ;

            TextView txt_food_rate = row.FindViewById<TextView>(Resource.Id.txt_model_rate);

            txt_food_rate.Text = ">>>>    Rates : $ " + Convert.ToString(mItems[position].Model_Rate );





            return row;


        }
    }
}