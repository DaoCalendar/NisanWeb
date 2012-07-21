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
                //insert into table Transactions
                this.Parent.CreatedAt = DateTime.Now;
                success &= this.Parent.Save();

                //insert into table TransactionItems
                success &= base.Save();

                //insert into table Nisan
                if(this.Stock is Nisan)
                    success &= (this.Stock as Nisan).Save();

                //insert into table Order
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO "+this.tableName;
                        command.CommandText += "(ItemId,NisanId,Status)";
                        command.CommandText += " VALUES(@ItemId,@NisanId,@Status);";
                        command.Parameters.Add(CreateParameter("@ItemId", this.idField));
                        command.Parameters.Add(CreateParameter("@NisanId", this.Stock.Id));
                        command.Parameters.Add(CreateParameter("@Status", this.statusField));
                        
                        object output = command.ExecuteScalar();
                        if (output != null)
                        {
                            this.idField = (int)output;
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
                        command.CommandText += " WHERE Id=@Id";
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
            //DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            //using (DbConnection connection = factory.CreateConnection())
            //{
            //    connection.ConnectionString = connectionString;
            //    connection.Open();

            //    //DataSet dataSet = new DataSet();
            //    //DbDataAdapter adapter = new DbDataAdapter(sql, connection);
            //    //adapter.Fill(dataSet);
            //    using (DbCommand command = connection.CreateCommand())
            //    {
            //        command.CommandType = System.Data.CommandType.Text;
            //        command.CommandText = "SELECT * FROM " + this.tableName+" WHERE 
            //        using (DbDataReader reader = command.ExecuteReader())
            //        {
            //            while (reader.Read())
            //            {
            //                //TODO: Cloning room
            //                Room room = new Room(
            //                reader["Name"].ToString(), reader["Email"].ToString());
            //                result.Add(room);
            //            }
            //        }
            //    }
            //}
            throw new NotImplementedException();
        }
        public Order Find(string name)
        {
            throw new NotImplementedException();

            //TODO: Get customer info
            //SELECT Users.*,Addresses.Street
            //FROM Users LEFT OUTER JOIN Addresses ON Users.AddressId=Addresses.Id
            //WHERE Users.Id = ??
        }
    }
}
