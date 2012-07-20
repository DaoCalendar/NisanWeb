using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class Nisan
    {
        public Nisan()
            : base()
        {
            Initialize();
        }
        private void Initialize()
        {
            this.tableName = "Nisans";
            this.nameField = string.Empty;
            this.jawiField = string.Empty;
            //this.bornField = new DateTime
            this.ageField = 0;

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
                        command.CommandText += "(Type,Name,Jawi,Born,Death,Deathm,Age,Remarks,Uri)";
                        command.CommandText += " VALUES(@Type,@Name,@Jawi,@Born,@Death,@Deathm,@Age,@Remarks,@Uri);";
                        command.CommandText += "SELECT SCOPE_IDENTITY;";
                        command.Parameters.Add(CreateParameter("@Type", base.Type));//stock.type. ie. 2' Batu Batik(L)
                        command.Parameters.Add(CreateParameter("@Name", this.nameField));
                        command.Parameters.Add(CreateParameter("@Jawi", this.jawiField));
                        command.Parameters.Add(CreateParameter("@Born", this.bornField));
                        command.Parameters.Add(CreateParameter("@Death", this.deathField));
                        command.Parameters.Add(CreateParameter("@Deathm", this.deathmField));
                        command.Parameters.Add(CreateParameter("@Age", this.ageField));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));

                        object output = command.ExecuteScalar();
                        if (output != null)
                        {
                            this.idField = Convert.ToInt32(output);
                            success = true;
                        }
                    }

                    connection.Close();
                }//end
            }
            else
            {
                //todo: update nisan
            }

            return success;
        }
        public override void Load()
        {
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