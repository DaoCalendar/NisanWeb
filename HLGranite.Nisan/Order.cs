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
            this.tableName = "TransactionItems";
            this.relatedItemsField = new List<TransactionItem>();
            this.agentField = new Agent();
            this.shipToField = new Address();
        }

        public override bool Save()
        {
            throw new NotImplementedException();
        }
        public List<Order> Own(string owner)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();

                //DataSet dataSet = new DataSet();
                //DbDataAdapter adapter = new DbDataAdapter(sql, connection);
                //adapter.Fill(dataSet);
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM " + this.tableName+" WHERE 
                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //TODO: Cloning room
                            Room room = new Room(
                            reader["Name"].ToString(), reader["Email"].ToString());
                            result.Add(room);
                        }
                    }
                }
            }
        }
        public Order Find(string name)
        {
            throw new NotImplementedException();
        }
    }
}
