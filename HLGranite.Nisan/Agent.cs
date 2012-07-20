using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Agent
    {
        private string codeField;
        /// <summary>
        /// Represent code for agent.
        /// </summary>
        public string Code
        {
            get { return this.codeField; }
            set { this.codeField = value; }
        }
        public Agent()
            : base()
        {
            this.codeField = string.Empty;
            this.typeField = Role.Agent;
            this.membersField = new List<Agent>();
            System.Diagnostics.Debug.WriteLine("-- Agent --");
        }
        public Agent(string code)
            : base()
        {
            this.codeField = code;
            this.typeField = Role.Agent;
            this.membersField = new List<Agent>();
            System.Diagnostics.Debug.WriteLine("-- Agent --");
        }
        public List<Order> GetSales()
        {
            throw new NotFiniteNumberException();
        }
    }
}