using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Designer
    {
        public Designer()
            : base()
        {
            this.typeField = Role.Designer;
            System.Diagnostics.Debug.WriteLine("-- Designer --");
        }
        public Designer(string code)
            : base()
        {
            this.typeField = Role.Designer;
            this.codeField = code;
            System.Diagnostics.Debug.WriteLine("-- Designer --");
        }
        public List<Order> GetWorkOrders()
        {
            throw new NotImplementedException();
        }
    }
}
