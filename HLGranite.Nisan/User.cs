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
            Initialize();
        }
        public User(int id)
            : base()
        {
            Initialize();
            this.idField = id;
            Load();
        }
        private void Initialize()
        {
            this.tableName = "Users";
            this.nameField = string.Empty;
            this.passwordField = string.Empty;
            this.phoneField = string.Empty;
            this.emailField = string.Empty;
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
                        command.Parameters.Add(CreateParameter("@Code", string.Empty));
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