﻿using System;
using System.Collections.Generic;
using System.Text;

namespace HLGranite.Nisan
{
    public partial class Transaction
    {
        public Transaction()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.tableName = "Transactions";
            this.createdByField = new User();
            this.itemsField = new List<TransactionItem>();
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