using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Admin
    {
        public Admin(string name)
            : base()
        {
            this.typeField = Role.Admin;
            this.Name = name;
            System.Diagnostics.Debug.WriteLine("-- Admin --");
        }
        public override bool Save()
        {
            throw new NotImplementedException();
        }

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }

        public void Pay(Agent agent, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}