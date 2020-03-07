using BusinessGame.Contracts.Enum;

namespace BusinessGame.Contracts
{
    public class Property
    {
        public PropertyType Type { get; set; }
        public int Penalty { get; set; }
        public string Name { get; set; }        

    }
    public class Hotel : Property
    {
        public string Owner { get; set; }
        public int Worth { get; set; }
        public bool IsOwned { get; set; }
    }
}
