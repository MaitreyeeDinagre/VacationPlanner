using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace VacationPlanner
{
    public class Event
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int EventID { get; set; }
        public string EventName { get; set; }
        public DateTime DepartureDate { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public string DepFrom { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public string ArrTo { get; set; }
        public string TravelMode { get; set; }
        public float TravelPrice { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public float StayPrice { get; set; }

        [ForeignKey(typeof(Trip))]
        public int TripID { get; set; }


    }
}