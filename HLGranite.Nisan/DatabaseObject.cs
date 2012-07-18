using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace HLGranite.Nisan
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(TransactionItem))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Delivery))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Commission))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Payment))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Order))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Transaction))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Stock))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Nisan))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(User))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Teller))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Carrier))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Designer))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Agent))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Customer))]
    public abstract class DatabaseObject
    {
        #region Properties
        /// <summary>
        /// Database provider name ie. MySql or PostGre.
        /// </summary>
        protected string providerName;
        /// <summary>
        /// Database connection string.
        /// </summary>
        protected string connectionString;
        private string idField;

        private string tableNameField;

        private string remarksField;

        public string Id
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        protected string tableName
        {
            get
            {
                return this.tableNameField;
            }
            set
            {
                this.tableNameField = value;
            }
        }

        public string Remarks
        {
            get
            {
                return this.remarksField;
            }
            set
            {
                this.remarksField = value;
            }
        }
        #endregion

        public DatabaseObject()
        {
            this.providerName = ConfigurationManager.ConnectionStrings["MatrixConnectionString"].ProviderName;
            this.connectionString = ConfigurationManager.ConnectionStrings["MatrixConnectionString"].ConnectionString;
        }

        #region Methods
        public abstract bool Save();
        public abstract void Load();
        public abstract bool Delete();
        #endregion
    }
}