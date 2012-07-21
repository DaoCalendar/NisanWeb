using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Admin
    {
        public Admin(string code)
            : base()
        {
            this.typeField = Role.Admin;
            this.codeField = code;
            System.Diagnostics.Debug.WriteLine("-- Admin --");
        }
        public List<Agent> LoadAllAgents()
        {
            throw new NotImplementedException();
        }
        public void Pay(Agent agent, decimal amount)
        {
            throw new NotImplementedException();
        }
    }
}