using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class User
    {
        public User()
            : base()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.tableName = "Users";
            this.addressField = new Address();
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
        public void Register()
        {
            throw new NotImplementedException();
        }
        public bool Login()
        {
            throw new NotImplementedException();
        }
    }
}