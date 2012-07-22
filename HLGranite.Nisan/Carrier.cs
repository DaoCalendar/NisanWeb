using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Carrier
    {
        public Carrier()
            : base()
        {
            this.typeField = Role.Carrier;
            System.Diagnostics.Debug.WriteLine("-- Carrier --");
        }
        public Carrier(string code)
            : base()
        {
            this.typeField = Role.Carrier;
            this.codeField = code;
            System.Diagnostics.Debug.WriteLine("-- Carrier --");
        }
        public void Deliver(Order order)
        {
            throw new NotImplementedException();
        }
    }
}