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

                //insert into table address
                success &= this.shipToField.Save();

                //insert into table Order
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO " + this.tableName;
                        command.CommandText += "(ItemId,Quantity,NisanId,AddressId,Status)";
                        command.CommandText += " VALUES(@ItemId,@Quantity,@NisanId,@AddressId,@Status);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@ItemId", this.idField));
                        command.Parameters.Add(CreateParameter("@NisanId", this.Stock.Id));
                        command.Parameters.Add(CreateParameter("@Quantity", this.quantityField));
                        command.Parameters.Add(CreateParameter("@AddressId", this.shipToField.Id));
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
        /// <summary>
        /// Find an last order with nisan death name with no case sensitive.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Order Find(string name)
        {
            Order order = new Order();
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    string sql =
@"SELECT Orders.Status
,Orders.AddressId,Addresses.Street,Addresses.Postal,Addresses.State
,Orders.Quantity,Stocks.Type,Stocks.Id AS StockId
,Nisans.*
,Users.Id,Users.Code,Users.Name AS Customer
,TransactionItems.Id AS TransactionItemId, TransactionItems.Amount
,Transactions.Id AS TransactionId,Transactions.No AS No, Transactions.CreatedAt
FROM Orders JOIN Nisans ON Orders.NisanId=Nisans.Id
JOIN Addresses ON Orders.AddressId=Addresses.Id
JOIN Stocks ON Nisans.Type = Stocks.Id
JOIN TransactionItems ON Orders.ItemId=TransactionItems.Id
JOIN Transactions ON TransactionItems.Parent=Transactions.Id
JOIN Users ON Transactions.CreatedBy=Users.Id
WHERE Nisans.Name=@Name
ORDER BY Transactions.CreatedAt DESC";
                    command.CommandText = sql;
                    command.Parameters.Add(CreateParameter("@Name", name));

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction parent = new Transaction();
                            parent.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

                            order.Parent = parent;
                            order.Status = (TransactionStage)Convert.ToInt32(reader["Status"]);
                            //todo: order.Agent = new Agent(reader["Code"].ToString());
                            order.Id = Convert.ToInt32(reader["TransactionItemId"]);
                            order.Amount = Convert.ToDecimal(reader["Amount"]);
                            order.Quantity = Convert.ToInt32(reader["Quantity"]);

                            Stock stock = new Stock(Convert.ToInt32(reader["StockId"]));
                            Nisan nisan = new Nisan(stock);
                            nisan.Type = reader["Type"].ToString();
                            nisan.Id = Convert.ToInt32(reader["Id"]);
                            nisan.Name = reader["Name"].ToString();
                            nisan.Death = Convert.ToDateTime(reader["Death"]);
                            if (reader["Deathm"] != DBNull.Value)
                                nisan.Deathm = ToMuslimDate(reader["Deathm"].ToString());
                            if (reader["Age"] != DBNull.Value)
                                nisan.Age = Convert.ToInt32(reader["Age"]);
                            if (reader["Born"] != DBNull.Value)
                                nisan.Born = Convert.ToDateTime(reader["Born"]);
                            nisan.Remarks = reader["Remarks"].ToString();
                            nisan.Uri = reader["Uri"].ToString();
                            order.Stock = nisan;

                            Address shipTo = new Address();
                            shipTo.Id = Convert.ToInt32(reader["AddressId"]);
                            shipTo.Street = reader["Street"].ToString();
                            shipTo.Postal = reader["Postal"].ToString();
                            shipTo.State = reader["State"].ToString();
                            order.ShipTo = shipTo;
                            //order.ShipTo = new Address((int)reader["AddressId"]);
                            break;//only take care of first record if there are several result return by desc
                        }
                    }
                }

                connection.Close();
            }//end

            return order;
        }
        private DateTime ToMuslimDate(string date)
        {
            int year = Convert.ToInt32(date.Substring(0, 4));
            int month = Convert.ToInt32(date.Substring(5, 2));
            int day = Convert.ToInt32(date.Substring(8, 2));
            return new DateTime(year, month, day);
        }
    }
}