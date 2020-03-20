using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;

namespace VacationPlanner
{
    public class Trip
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int TripID { get; set; }
        public string TripName { get; set; }
        public string TripFrom { get; set; }
        public string TripTo { get; set; }
        public int Days { get; set; }
        public string Details { get; set; }
        public float Budget { get; set; }
        public DateTime DatePicker { get; set; }

        public Boolean IsCancel { get; set; }



        [OneToMany]
        public List<Event> Events { get; set; }
    }
}