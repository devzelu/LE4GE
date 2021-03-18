using le4ge.Utilites.Custom;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.WindowsRuntime;

namespace le4ge
{
    public class DatabaseManager : IManager
    {
        
        var server = "";
        var database = "";
        var uid = "";
        var password = "";

        //Setup connection string
        public void Initialize()
        {
        }

        public void SavePosition(int characterID, string position)
        {
            try
            {
                var sql = $"UPDATE characters SET last_position='{position}' WHERE characters.model_id='{characterID}'";
                var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PWD=" + password + ";";
                using var con = new MySqlConnection(connectionString);
                using var cmd = new MySqlCommand(sql, con);
                MySqlDataReader myReader;
                con.Open();
                myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                }

                con.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public List<CustomCharacter> SelectInfo(ulong Sid)
        {

            List<CustomCharacter> playerInfo = new List<CustomCharacter>();
            try
            {
                var sql = $"SELECT * FROM characters JOIN clients ON characters.client_global_id=clients.global_id WHERE clients.socialclub_id={Sid} AND characters.isActive=1";
                var connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PWD=" + password + ";";
                using var con = new MySqlConnection(connectionString);
                con.Open();

                using var cmd = new MySqlCommand(sql, con);

                using MySqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    playerInfo.Add(new CustomCharacter
                    {
                        ClientID = int.Parse(rdr["client_global_id"].ToString()),
                        CharacterID = int.Parse(rdr["model_id"].ToString()),
                        FirstName = rdr["firstName"].ToString(),
                        LastName = rdr["lastName"].ToString(),
                        Date = (rdr["date"].ToString()).Split(' ')[0],
                        Age = int.Parse(rdr["age"].ToString()),
                        Position = rdr["last_position"].ToString(),
                        isJailed = Convert.ToBoolean(rdr["isJailed"]),
                        Gander = rdr["gender"].ToString(),
                        Energy = int.Parse(rdr["energy"].ToString()),
                        Hunger = int.Parse(rdr["hunger"].ToString()),
                        Thirst = int.Parse(rdr["thirst"].ToString()),
                        Work = rdr["work"].ToString(),
                        Cash = int.Parse(rdr["cash"].ToString()),
                        Bank = int.Parse(rdr["bank"].ToString()),
                        isAdmin = Convert.ToBoolean(rdr["isAdmin"]),
                        Model = rdr["model"].ToString()
                    });
                }
                con.Close();
                return playerInfo;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }
}
