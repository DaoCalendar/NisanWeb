using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class TransactionItem
    {
        public TransactionItem()
        {
            this.tableName = "TransactionItems";
            this.parentField = new Transaction();
            this.amountField = 0m;
            this.stockField = new Stock();
        }
        public override bool Save()
        {
            bool success = true;
            if (this.idField == 0)
            {
                using (DbConnection connection = factory.CreateConnection())
                {
                    connection.ConnectionString = connectionString;
                    connection.Open();

                    //insert TransactionItems
                    using (DbCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO " + this.tableName;
                        command.CommandText += "(Type,Parent,Amount,Remarks,Uri)";
                        command.CommandText += " VALUES(@Type,@Parent,@Amount,@Remarks,@Uri);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@Type", (int)TransactionType.Order));
                        command.Parameters.Add(CreateParameter("@Parent", this.Parent.Id));
                        command.Parameters.Add(CreateParameter("@Amount", this.Amount));
                        command.Parameters.Add(CreateParameter("@Remarks", this.remarksField));
                        command.Parameters.Add(CreateParameter("@Uri", this.uriField));

                        object output = command.ExecuteScalar();
                        if (output != null)
                        {
                            this.idField = (int)output;
                            success = true;
                        }
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

        public override void Load()
        {
            throw new NotImplementedException();
        }

        public override bool Delete()
        {
            throw new NotImplementedException();
        }
    }
}
