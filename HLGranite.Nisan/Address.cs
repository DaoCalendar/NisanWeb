using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class Address
    {
        public Address()
            : base()
        {
            Initialize();
        }
        public Address(int id)
            : base()
        {
            Initialize();
            this.idField = id;
            Load();
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
                        command.CommandText = "INSERT INTO " + this.tableName;
                        command.CommandText += "(Street,Postal,State,Remarks,Uri)";
                        command.CommandText += " VALUES(@Street,@Postal,@State,@Remarks,@Uri);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@Street", this.streetField));
                        command.Parameters.Add(CreateParameter("@Postal", this.postalField));
                        command.Parameters.Add(CreateParameter("@State", this.stateField));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));

                        object result = command.ExecuteScalar();
                        if (result != null)
                        {
                            this.idField = Convert.ToInt32(result);
                            success = true;
                        }
                    }

                    connection.Close();
                }//end
            }
            else
            {
                //todo: update address
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
                        command.CommandText = "SELECT * FROM " + this.tableName + " WHERE Id=" + this.idField + ";";

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.idField = (int)reader["Id"];
                            this.streetField = reader["Street"].ToString();
                            this.postalField = reader["Postal"].ToString();
                            this.stateField = reader["State"].ToString();
                            this.remarksField = reader["Remarks"].ToString();
                            this.uriField = reader["Uri"].ToString();
                        }
                    }
                }

                connection.Close();
            }//end
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}