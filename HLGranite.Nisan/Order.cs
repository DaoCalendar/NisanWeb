using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class Order
    {
        public Order()
        {
            this.tableName = "Orders";
            this.relatedItemsField = new List<TransactionItem>();
            this.agentField = new Agent();
            this.shipToField = new Address();

            this.Parent = new Transaction();
            this.Parent.Type = TransactionType.Order;
        }

        public override bool Save()
        {
            bool success = true;
            if (this.idField == 0)
            {
                if (this.Parent.CreatedBy is Customer)
                    this.Parent.CreatedBy.Save();
                //insert into table Transactions
                this.Parent.CreatedAt = DateTime.Now;
                if (this.Parent.CreatedBy == null || this.Parent.CreatedBy.Name.Length == 0)
                    this.Parent.CreatedBy = this.agentField;
                success &= this.Parent.Save();

                //insert into table TransactionItems
                success &= base.Save();

                //insert into table Nisan
                if (this.Stock is Nisan)
                    success &= (this.Stock as Nisan).Save();

                //insert into table Order
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO " + this.tableName;
                        command.CommandText += "(ItemId,NisanId,Status)";
                        command.CommandText += " VALUES(@ItemId,@NisanId,@Status);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@ItemId", this.idField));
                        command.Parameters.Add(CreateParameter("@NisanId", this.Stock.Id));
                        command.Parameters.Add(CreateParameter("@Status", this.statusField));

                        object output = command.ExecuteScalar();
                        if (output != null)
                        {
                            this.idField = Convert.ToInt32(output);
                            success &= true;
                        }
                        else
                            success &= false;
                    }

                    connection.Close();
                }//end
            }
            else
            {
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "UPDATE TransactionItems";
                        command.CommandText += " SET Amount=@Amount,Remarks=@Remarks,Uri=@Uri";
                        command.CommandText += " WHERE Id=@Id;";
                        command.Parameters.Add(CreateParameter("@Id", this.idField));
                        command.Parameters.Add(CreateParameter("@Amount", this.Amount));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));

                        success = (command.ExecuteNonQuery() > 0) ? true : false;
                    }

                    connection.Close();
                }//end
            }

            return success;
        }
        public List<Order> Own(string owner)
        {
            throw new NotImplementedException();
        }
        public Order Find(string name)
        {
            throw new NotImplementedException();

            //TODO: Get customer info
            //SELECT Users.*,Addresses.Street
            //FROM Users LEFT OUTER JOIN Addresses ON Users.AddressId=Addresses.Id
            //WHERE Users.Id = ??

            /**
             * SELECT Orders.Status,Nisans.*
,Users.Id,Users.Code,Users.Name AS Customer
,TransactionItems.Id AS TransactionItemId
,Transactions.Id AS TransactionId,Transactions.No AS No
FROM Orders JOIN Nisans ON Orders.NisanId=Nisans.Id
JOIN TransactionItems ON Orders.ItemId=TransactionItems.Id
JOIN Transactions ON TransactionItems.Parent=Transactions.Id
JOIN Users ON Transactions.CreatedBy=Users.Id
--WHERE Nisans.Id=14;
--WHERE Nisans.Name = 'Roslan34 bt Mohd'
WHERE Users.Name='Ali'
--WHERE Users.Code='W002';

--select * from nisans;
             */
        }
    }
}