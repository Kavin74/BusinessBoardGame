namespace BusinessGame.Contracts
{
    public class Player
    {
        public string Name { get; set; }
        public int Money { get; set; }
        public int HotelAssest { get; set; }
        public int Position { get; set; }
        public int TotalChances { get; set; }
        public int TotalWorth { get { return HotelAssest + Money; } }
    }
}
