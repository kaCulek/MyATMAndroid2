using System.Collections.Generic;

namespace MyATMAndroid
{
    public class AtmUser
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public List<AtmUserTransaction> Transactions { get; set; }
    }
}