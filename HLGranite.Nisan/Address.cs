using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Address
    {
        public Address()
            : base()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.tableName = "Addresses";
            this.streetField = string.Empty;
            this.postalField = string.Empty;
            this.stateField = string.Empty;
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
    }
}