using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Nisan
    {
        public Nisan():base()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.tableName = "Nisans";
            this.nameField = string.Empty;
            this.jawiField = string.Empty;
            //this.bornField = new DateTime
            this.ageField = 0;

        }
    }
}
