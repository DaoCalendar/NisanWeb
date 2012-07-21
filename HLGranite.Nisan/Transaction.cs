using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class Transaction
    {
        public Transaction()
            : base()
        {
            Initialize();
        }

        private void Initialize()
        {
            this.tableName = "Transactions";
            this.createdByField = new User();
            this.noField = string.Empty;
            this.referenceField = string.Empty;
            this.itemsField = new List<TransactionItem>();
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
                        command.CommandText = "INSERT INTO Transactions(Type,No,CreatedAt,CreatedBy,Reference,Remarks,Uri)";
                        command.CommandText += " VALUES(@Type,@No,@CreatedAt,@CreatedBy,@Reference,@Remarks,@Uri);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@Type", (int)this.typeField));
                        command.Parameters.Add(CreateParameter("@No", this.noField));
                        command.Parameters.Add(CreateParameter("@CreatedAt", this.createdAtField));
                        command.Parameters.Add(CreateParameter("@CreatedBy", this.createdByField.Id));
                        command.Parameters.Add(CreateParameter("@Reference", this.referenceField));
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
                //do nothing. Transaction header once created is not allow modify anymore.
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