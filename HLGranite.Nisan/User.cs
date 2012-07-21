using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class User
    {
        public User()
            : base()
        {
            System.Diagnostics.Debug.WriteLine("-- User --");
            Initialize();
        }
        public User(int id)
            : base()
        {
            Initialize();
            this.idField = id;
            Load();
        }
        public User(string code)
            : base()
        {
            Initialize();
            this.codeField = code;
            Load();
        }
        private void Initialize()
        {
            System.Diagnostics.Debug.WriteLine("-- User.Initialize --");
            this.tableName = "Users";
            this.codeField = string.Empty;
            this.nameField = string.Empty;
            this.passwordField = string.Empty;
            this.phoneField = string.Empty;
            this.emailField = string.Empty;
            this.typeField = Role.Customer;//security purpose just in case default creation is admin
            //this.addressField = new Address();
        }
        public override bool Save()
        {
            bool success = true;
            if (this.idField == 0)
            {
                int addressId = 0;
                if (this.addressField != null)
                {
                    success &= this.addressField.Save();
                    addressId = this.addressField.Id;
                }

                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();
                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO " + this.tableName;
                        command.CommandText += "(Type,Code,Name,Password,Email,Phone,AddressId,Remarks,Uri)";
                        command.CommandText += " VALUES(@Type,@Code,@Name,@Password,@Email,@Phone,@AddressId,@Remarks,@Uri);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@Type", this.typeField));
                        command.Parameters.Add(CreateParameter("@Code", this.codeField));
                        command.Parameters.Add(CreateParameter("@Name", this.nameField));
                        command.Parameters.Add(CreateParameter("@Password", this.passwordField));
                        command.Parameters.Add(CreateParameter("@Email", this.emailField));
                        command.Parameters.Add(CreateParameter("@Phone", this.phoneField));
                        command.Parameters.Add(CreateParameter("@AddressId", addressId));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));

                        object result = command.ExecuteScalar();
                        if (result == null)
                            success &= false;
                        else
                        {
                            this.idField = Convert.ToInt32(result);
                            success &= true;
                        }
                    }

                    connection.Close();
                }//end
            }
            else
            {
                //todo: update user
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
                    else if (this.codeField.Length > 0)
                    {
                        command.CommandText = "SELECT * FROM " + this.tableName + " WHERE Code=@Code;";
                        command.Parameters.Add(CreateParameter("@Code", this.codeField));
                    }

                    using (DbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            this.idField = (int)reader["Id"];
                            this.codeField = reader["Code"].ToString();
                            this.nameField = reader["Name"].ToString();
                            this.passwordField = reader["Password"].ToString();
                            this.emailField = reader["Email"].ToString();
                            this.phoneField = reader["Phone"].ToString();
                            this.remarksField = reader["Remarks"].ToString();
                            this.uriField = reader["Uri"].ToString();

                            if (reader["AddressId"] != DBNull.Value)
                                this.addressField = new Address((int)reader["AddressId"]);
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

        public bool Register()
        {
            return Save();
        }
        public bool Login(string password)
        {
            Load();
            if (this.idField == 0) return false;
            return this.passwordField.Equals(password);
        }
    }
}