using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Agent
    {
        public Agent()
            : base()
        {
            this.typeField = Role.Agent;
            System.Diagnostics.Debug.WriteLine("-- Agent --");
            //this.membersField = new List<Agent>();
        }
        public Agent(string code)
            : base()
        {
            System.Diagnostics.Debug.WriteLine("-- Agent --");
            this.Code = code;
            this.typeField = Role.Agent;
            //this.membersField = new List<Agent>();
            base.Load();
        }
        public List<Order> GetSales()
        {
            throw new NotFiniteNumberException();
        }
    }
}