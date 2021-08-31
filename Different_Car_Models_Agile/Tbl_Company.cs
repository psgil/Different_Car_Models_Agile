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
    public class Tbl_Company
    {
        [PrimaryKey, AutoIncrement] //Column("Id")]
        public int Id { get; set; }

        [MaxLength(25)]
        public string Company_Name { get; set; }
    }
}