using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using Pharmacy.Common;

namespace Pharmacy.Dal
{
    public class GenericName
    {
        private static string host = "localhost";
        private static string database = "pharmacy_db";
        private static string user = "root";
        private static string pass = "";

        private string _connectionString = "Server=" + host + "; Database=" + database + "; Uid=" + user + "; Password=" + pass + ";";
        private static MySqlConnection _connection;

        public void DbConnection()
        {
            _connection = new MySqlConnection(_connectionString);
            if (_connection.State==ConnectionState.Closed || _connection.State==ConnectionState.Broken)
            {
                _connection.Open();
            }
            else
            {
                _connection.Close();
            }

            
        }

        public bool QueryExecute(string query)
        {
            DbConnection();
            var cmd = new MySqlCommand(query, _connection);
            return cmd.ExecuteNonQuery() > 0;
        }

        public bool GetDataTable(string query)
        {
            var cmd = new MySqlCommand(query, _connection);
            var da=new MySqlDataAdapter(cmd);
            var dt=new DataTable();
            da.Fill(dt);
            return dt.Rows.Count > 0;
        }

        public List<Models.GenericName> ListOfGenericNames()
        {
            DbConnection();
            var genericNameList=new List<Models.GenericName>();
            var cmd = new MySqlCommand("SELECT * FROM tbl_generic_name", _connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count>0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var generic = new Models.GenericName();
                    generic.Id = Convert.ToInt32(row["id"].ToString());
                    generic.Name = row["name"].ToString();

                    genericNameList.Add(generic);
                }
            }

            return genericNameList;
        }

        public bool HasExist(Models.GenericName genericName)
        {
            DbConnection();
            var cmd = new MySqlCommand("SELECT * FROM tbl_generic_name WHERE name='" + genericName.Name + "'", _connection);
            var da = new MySqlDataAdapter(cmd);
            var dt = new DataTable();
            da.Fill(dt);
            return dt.Rows.Count > 0;
        }
    }
}