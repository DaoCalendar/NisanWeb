using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Data.Common;

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
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Admin))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Carrier))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Designer))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Agent))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(Customer))]
    public abstract class DatabaseObject
    {
        #region Properties
        /// <summary>
        /// Obsolete. file:// For storing uri path purpose.
        /// </summary>
        protected const string URI_PREFIX = "file://";
        protected DbProviderFactory factory;
        /// <summary>
        /// Database provider name ie. MySql or PostGre.
        /// </summary>
        protected string providerName;
        /// <summary>
        /// Database connection string.
        /// </summary>
        protected string connectionString;
        /// <summary>
        /// Display message when there is an error.
        /// </summary>
        protected string message;

        protected int idField;

        protected string tableNameField;

        protected string remarksField;

        protected string uriField;

        public int Id
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

        public string Uri
        {
            get { return this.uriField; }
            set { this.uriField = value; }
        }
        #endregion

        public DatabaseObject()
        {
            this.idField = 0;
            this.tableNameField = string.Empty;
            this.remarksField = string.Empty;
            this.uriField = string.Empty;
            this.providerName = ConfigurationManager.ConnectionStrings["NisanConnectionString"].ProviderName;
            this.connectionString = ConfigurationManager.ConnectionStrings["NisanConnectionString"].ConnectionString;
            this.factory = DbProviderFactories.GetFactory(providerName);
        }

        #region Methods
        /// <summary>
        /// Returns sql parameter based on provider.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected DbParameter CreateParameter(string name, object value)
        {
            DbProviderFactory factory = DbProviderFactories.GetFactory(providerName);
            DbParameter parameter = factory.CreateParameter();
            parameter.ParameterName = name;
            parameter.Value = value;
            return parameter;
        }

        //todo: public abstract bool Validate();
        public abstract bool Save();
        public abstract void Load();
        public abstract bool Delete();
        #endregion
    }
}