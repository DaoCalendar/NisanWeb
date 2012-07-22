using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

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
            return base.GetSales(this.codeField);
        }
        public Dictionary<Order, decimal> GetCommission()
        {
            throw new NotFiniteNumberException();
        }
    }
}