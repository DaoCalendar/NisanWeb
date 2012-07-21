using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace HLGranite.Nisan
{
    public partial class Nisan
    {
        /// <summary>
        /// Stored stock id.
        /// </summary>
        private int stockId;
        public Nisan()
            : base()
        {
            Initialize();
            System.Diagnostics.Debug.WriteLine("-- Nisan --");
        }
        /// <summary>
        /// Recommended constructor. Clone from stock object.
        /// </summary>
        /// <param name="stock"></param>
        public Nisan(Stock stock)
            : base(stock.Id)
        {
            Initialize();
            Clone(stock);
            this.stockId = stock.Id;
            System.Diagnostics.Debug.WriteLine("-- Nisan --");
        }
        /// <summary>
        /// TODO: Why need this additional step?
        /// </summary>
        /// <param name="stock"></param>
        private void Clone(Stock stock)
        {
            base.Type = stock.Type;
            //base.remarksField = stock.Remarks;
            //base.uriField = stock.Uri;
        }
        private void Initialize()
        {
            this.stockId = 0;
            this.idField = 0;//to reset clone from stock
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
                        object born = DBNull.Value;
                        object death = DBNull.Value;
                        object deathm = DBNull.Value;
                        if (this.bornField != DateTime.MinValue) born = this.bornField;
                        if (this.deathField != DateTime.MinValue) death = this.deathField;
                        if (this.deathmField != DateTime.MinValue) deathm = this.deathmField;

                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = "INSERT INTO " + this.tableName;
                        command.CommandText += "(Type,Name,Jawi,Born,Death,Deathm,Age,Remarks,Uri)";
                        command.CommandText += " VALUES(@Type,@Name,@Jawi,@Born,@Death,@Deathm,@Age,@Remarks,@Uri);";
                        command.CommandText += "SELECT SCOPE_IDENTITY();";
                        command.Parameters.Add(CreateParameter("@Type", this.stockId));//stock.type. ie. 2' Batu Batik(L)
                        command.Parameters.Add(CreateParameter("@Name", this.nameField));
                        command.Parameters.Add(CreateParameter("@Jawi", this.jawiField));
                        command.Parameters.Add(CreateParameter("@Born", born));
                        command.Parameters.Add(CreateParameter("@Death", death));                       
                        command.Parameters.Add(CreateParameter("@Deathm", deathm));
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