using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arm_shop
{
    internal class SqlCommands
    {
        public string strConnection = Config.stringConnection;
        public NpgsqlConnection strCon;

        public SqlCommands()
        {
            try
            {
                strCon = new NpgsqlConnection();
                strCon.ConnectionString = strConnection;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void Connection()
        {
            if (strCon.State == ConnectionState.Closed)
            {
                strCon.Open();
            }
        }

        public DataTable GetData(string sql)
        {
            DataTable dt = new DataTable();
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }
            return dt;

        }

        public DataTable GetData<K>(string sql, K parametrs, string placeholder)
        {
            DataTable dt = new DataTable();
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder, parametrs);
                NpgsqlDataReader dr = cmd.ExecuteReader();
                dt.Load(dr);
            }

            return dt;
        }

        public void AddData(string sql, (string, int) parametrs, (string, string) placeholder)
        {
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder.Item1, parametrs.Item1);
                cmd.Parameters.AddWithValue(placeholder.Item2, parametrs.Item2);

                cmd.ExecuteNonQuery();
            }
        }
        public void AddData(string sql, string parametrs, string placeholder)
        {
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder, parametrs);

                cmd.ExecuteNonQuery();
            }
        }

        public void AddData(string sql, (string, string, int) parametrs, (string, string, string) placeholder)
        {
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder.Item1, parametrs.Item1);
                cmd.Parameters.AddWithValue(placeholder.Item2, parametrs.Item2);
                cmd.Parameters.AddWithValue(placeholder.Item3, parametrs.Item3);

                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteData(string sql, int id, string placeholder)
        {
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder, id);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateData(string sql, string name, int id, (string, string) placeholder)
        {
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder.Item1, name);
                cmd.Parameters.AddWithValue(placeholder.Item2, id);

                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateData(string sql, (string, string) info, int id, (string, string, string) placeholder)
        {
            Connection();
            using (var cmd = new NpgsqlCommand(sql, strCon))
            {
                cmd.Parameters.AddWithValue(placeholder.Item1, info.Item1);
                cmd.Parameters.AddWithValue(placeholder.Item2, info.Item2);
                cmd.Parameters.AddWithValue(placeholder.Item3, id);

                cmd.ExecuteNonQuery();
            }

        }
    }
}
