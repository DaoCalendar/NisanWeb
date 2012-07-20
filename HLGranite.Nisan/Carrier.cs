using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Carrier
    {
        public Carrier(string name)
            : base()
        {
            this.typeField = Role.Carrier;
            this.Name = name;
            System.Diagnostics.Debug.WriteLine("-- Carrier --");
        }
        public void Deliver(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
