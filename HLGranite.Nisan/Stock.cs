using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace HLGranite.Nisan
{
    public partial class Stock
    {
        public Stock()
            : base()
        {
            Initialize();
            System.Diagnostics.Debug.WriteLine("-- Stock --");
        }
        /// <summary>
        /// Constructor for retrieve.
        /// </summary>
        /// <param name="id"></param>
        public Stock(int id)
            : base()
        {
            Initialize();
            this.idField = id;
            Load();
            System.Diagnostics.Debug.WriteLine("-- Stock --");
        }
        /// <summary>
        /// Constructor for retrieve.
        /// </summary>
        /// <param name="name"></param>
        public Stock(string name)
            : base()
        {
            Initialize();
            this.typeField = name;
            Load();
            System.Diagnostics.Debug.WriteLine("-- Stock --");
        }

        private void Initialize()
        {
            this.tableName = "Stocks";
            this.typeField = string.Empty;
            this.priceField = 0M;
        }
        public override bool Save()
        {
            bool success = false;
            if (this.idField == 0)
            {
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO " + this.tableName + "(Type,Price,Remarks,Uri)";
                        command.CommandText += " VALUES(@Type,@Price,@Remarks,@Uri)";
                        command.Parameters.Add(CreateParameter("@Type", this.typeField));
                        command.Parameters.Add(CreateParameter("@Price", this.priceField));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));

                        success = (command.ExecuteNonQuery() > 0) ? true : false;
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
                        command.CommandText = "UPDATE " + this.tableName;
                        command.CommandText += " SET Type=@Type,Price=@Price,Remarks=@Remarks,Uri=@Uri";
                        command.CommandText += " WHERE Id=@Id";
                        command.Parameters.Add(CreateParameter("@Id", this.idField));
                        command.Parameters.Add(CreateParameter("@Type", this.typeField));
                        command.Parameters.Add(CreateParameter("@Price", this.priceField));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));//Uri.Host

                        success = (command.ExecuteNonQuery() > 0) ? true : false;
                    }

                    connection.Close();
                }//end
            }

            return success;
        }
        public override void Load()
        {
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    if (this.idField > 0)
                        command.CommandText = "SELECT * FROM " + this.tableName + " WHERE Id=" + this.idField;
                    else if (this.typeField.Length > 0)
                    {
                        command.CommandText = "SELECT * FROM " + this.tableName + " WHERE Type=@Type";
                        command.Parameters.Add(CreateParameter("@Type", this.typeField));
                    }

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.idField = (int)reader["Id"];
                            this.typeField = reader["Type"].ToString();
                            this.priceField = (decimal)reader["Price"];
                            this.remarksField = reader["Remarks"].ToString();
                            this.uriField = reader["Uri"].ToString();
                        }
                    }
                }

                connection.Close();
            }//end
        }
        public List<Stock> LoadAll()
        {
            List<Stock> result = new List<Stock>();
            using (DbConnection connection = factory.CreateConnection())
            {
                connection.ConnectionString = this.connectionString;
                connection.Open();
                using (DbCommand command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM " + base.tableName;
                    using (DbDataAdapter adapter = factory.CreateDataAdapter())
                    {
                        DataSet dataSet = new DataSet();
                        adapter.SelectCommand = command;
                        adapter.SelectCommand.Connection = connection;
                        adapter.Fill(dataSet);
                        if (dataSet.Tables.Count > 0)
                        {
                            foreach (DataRow row in dataSet.Tables[0].Rows)
                            {
                                Stock stock = new Stock();
                                stock.Id = (int)row["Id"];
                                stock.Type = row["Type"].ToString();
                                stock.Price = (decimal)row["Price"];
                                stock.Remarks = row["Remarks"].ToString();
                                stock.Uri = row["Uri"].ToString();
                                result.Add(stock);
                            }
                        }
                        dataSet.Clear();
                        dataSet.Dispose();
                    }
                }

                connection.Close();
            }//end

            return result;
        }
        /// <summary>
        /// TODO: Stock.Delete.
        /// </summary>
        /// <returns></returns>
        public override bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}