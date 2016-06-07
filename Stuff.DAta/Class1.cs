using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stuff.DAta
{
    public class Stuff
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Image { get; set; }
    }

    public class StuffManager
    {
        private string _connectionString;

        public StuffManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Stuff> GetStuff()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM Stuffs";
                List<Stuff> stuffs = new List<Stuff>();
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    stuffs.Add(new Stuff
                    {
                        Id = (int)reader["Id"],
                        Image = (string)reader["Image"],
                        Word = (string)reader["Word"]
                    });
                }

                return stuffs;
            }
        }

        public void AddStuff(string image, string word)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Stuffs (Word, Image) VALUES (@word, @image)";
                cmd.Parameters.AddWithValue("@word", word);
                cmd.Parameters.AddWithValue("@image", image);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
