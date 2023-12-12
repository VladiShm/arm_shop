using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace arm_shop
{
    internal class Autorization
    {
        static public string role, login_a, password_a;
        static public int acc_id = 0;

        //генерация хэша пароля
        public static string GenerateHash(string password)
        {
            var enc = Encoding.GetEncoding(0);
            byte[] buffer = enc.GetBytes(password);
            var sha1 = SHA1.Create();
            var hash = BitConverter.ToString(sha1.ComputeHash(buffer));
            return hash;
        }

        public static string NewHash(string hash)
        {
            return GenerateHash(hash + "Чуприна");
        }
        static public void AutorizationMethod(string login, string password)
        {
            try
            {
                role = password_a = login_a = null;
                password = NewHash(GenerateHash(password));
                SqlCommands commands = new SqlCommands();
                commands.Connection();
                NpgsqlDataReader result;
                var command = "SELECT a.id, role, login, password from role join accounts as a on role.id = id_role WHERE login = @login and password = @password";
                using (var cmd = new NpgsqlCommand(command, commands.strCon))
                {
                    cmd.Parameters.AddWithValue("@login", login);
                    cmd.Parameters.AddWithValue("@password", password);
                    result = cmd.ExecuteReader();
                }
                if (result.Read())
                {
                    acc_id = int.Parse(result["id"].ToString());
                    role = result["role"].ToString();
                    login_a = result["login"].ToString();
                    password_a = result["Password"].ToString();
                }
            }
            catch
            {
                role = null;
                MessageBox.Show("Ошибка при авторизации!");
            }
        }
    }
}
