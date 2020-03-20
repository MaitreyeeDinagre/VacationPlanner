using SQLiteNetExtensions.Attributes;

namespace VacationPlanner
{
    public class ShoppingBagModel
    {
        [SQLite.PrimaryKey, SQLite.AutoIncrement]
        public int BagID { get; set; }
        public string ItemName { get; set; }
        public string Quantity { get; set; }

        [ForeignKey(typeof(Trip))]
        public int TripID { get; set; }
    }
}
