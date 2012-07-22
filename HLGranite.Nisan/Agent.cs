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
            List<Order> sales = new List<Order>();
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
,Orders.Quantity,Stocks.Type
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
WHERE Users.Code=@Code;";
                    command.CommandText = sql;
                    command.Parameters.Add(CreateParameter("@Code", this.codeField));

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Transaction parent = new Transaction();
                            parent.CreatedAt = Convert.ToDateTime(reader["CreatedAt"]);

                            Order order = new Order();
                            order.Parent = parent;
                            order.Status = (TransactionStage)Convert.ToInt32(reader["Status"]);
                            order.Agent = this;
                            order.Id = Convert.ToInt32(reader["TransactionItemId"]);
                            order.Amount = Convert.ToDecimal(reader["Amount"]);
                            order.Quantity = Convert.ToInt32(reader["Quantity"]);

                            Nisan nisan = new Nisan();
                            nisan.Type = reader["Type"].ToString();
                            nisan.Id = Convert.ToInt32(reader["Id"]);
                            nisan.Name = reader["Name"].ToString();
                            nisan.Death = Convert.ToDateTime(reader["Death"]);
                            if(reader["Deathm"] != DBNull.Value)
                                nisan.Deathm = ToMuslimDate(reader["Deathm"].ToString());
                            if (reader["Age"] != DBNull.Value)
                                nisan.Age = Convert.ToInt32(reader["Age"]);
                            if (reader["Born"] != DBNull.Value)
                                nisan.Born = Convert.ToDateTime(reader["Born"]);
                            nisan.Remarks = reader["Remarks"].ToString();
                            nisan.Uri = reader["Uri"].ToString();
                            order.Stock = nisan;

                            Address shipTo = new Address();
                            shipTo.Street = reader["Street"].ToString();
                            shipTo.Postal = reader["Postal"].ToString();
                            shipTo.State = reader["State"].ToString();
                            order.ShipTo = shipTo;
                            //order.ShipTo = new Address((int)reader["AddressId"]);

                            sales.Add(order);
                        }
                    }
                }

                connection.Close();
            }//end

            return sales;
        }
        public Dictionary<Order, decimal> GetCommission()
        {
            throw new NotFiniteNumberException();
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