using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Designer
    {
        public Designer(string name)
            : base()
        {
            this.typeField = Role.Designer;
            this.Name = name;
            System.Diagnostics.Debug.WriteLine("-- Designer --");
        }
        public List<Order> GetWorkOrders()
        {
            throw new NotImplementedException();
        }
    }
}
