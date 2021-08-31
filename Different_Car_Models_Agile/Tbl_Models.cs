using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Different_Car_Models_Agile
{
    public class Tbl_Models
    {
        [PrimaryKey, AutoIncrement] //Column("Id")]
        public int Id { get; set; }
        public int Cid { get; set; }
        public string Model_Name { get; set; }

        public int Model_Rate { get; set; }
    }
}