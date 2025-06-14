using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient; 

namespace Wpf_BoostCSharp
{
    class GestionChansons
    {

        private readonly string _connectionString = @"Data Source=(LocalDB)\v11.0;AttachDbFilename='C:\Users\My-Vio\Documents\Visual Studio 2013\Projects\Wpf_BoostCSharp\Wpf_BoostCSharp\Boost.mdf';Integrated Security=True;";

        // Create
        public void AddMusic(Chansons music)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "INSERT INTO Chansons (Titre, Auteurs, Type, Instruments) VALUES (@Titre, @Auteurs, @Type, @Instruments)";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Titre", music.Titre);
                    command.Parameters.AddWithValue("@Auteurs", music.Auteurs ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Type", music.Type ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Instruments", music.Instruments ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Read
        public List<Chansons> GetAllMusic()
        {
            var musicList = new List<Chansons>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Chansons";
                using (var command = new SqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            musicList.Add(new Chansons
                            {
                                Id = reader.GetInt32(0),
                                Titre = reader.GetString(1),
                                Auteurs = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Type = reader.IsDBNull(3) ? null : reader.GetString(3),
                                Instruments = reader.IsDBNull(4) ? null : reader.GetString(4)
                            });
                        }
                    }
                }
            }
            return musicList;
        }

        // Update
        public void UpdateMusic(Chansons music)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "UPDATE Chansons SET Titre=@Titre, Auteurs=@Auteurs, Type=@Type, Instruments=@Instruments WHERE Id=@Id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", music.Id);
                    command.Parameters.AddWithValue("@Titre", music.Titre);
                    command.Parameters.AddWithValue("@Auteurs", music.Auteurs ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Type", music.Type ?? (object)DBNull.Value);
                    command.Parameters.AddWithValue("@Instruments", music.Instruments ?? (object)DBNull.Value);
                    command.ExecuteNonQuery();
                }
            }
        }

        // Delete
        public void DeleteMusic(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string sql = "DELETE FROM Chansons WHERE Id=@Id";
                using (var command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

    }

}        

   