using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace HLGranite.Nisan
{
    public class State
    {
        /// <summary>
        /// Returns all states for address use.
        /// </summary>
        /// <returns></returns>
        public static string[] GetAll()
        {
            List<string> result = new List<string>();
            string file = AppDomain.CurrentDomain.BaseDirectory
                + System.IO.Path.DirectorySeparatorChar + "App_Data"
                + Path.DirectorySeparatorChar + "states.xml";
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(file);
            
            if (dataSet.Tables.Count == 0) return result.ToArray();
            foreach (DataRow row in dataSet.Tables[0].Rows)
                result.Add(row["Name"].ToString());

            return result.ToArray();
        }
    }
}