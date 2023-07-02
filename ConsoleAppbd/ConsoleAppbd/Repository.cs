using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppbd
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.SqlClient;

    public class Repository<T> : IRepository<T> where T : class
    {
        private string connectionString;

        public Repository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<T> GetAll()
        {
            List<T> entities = new List<T>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string typeName = typeof(T).Name;
                string procedureName = $"usp_Get{typeName}s";
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            T entity = Activator.CreateInstance<T>();
                            foreach (var property in typeof(T).GetProperties())
                            {
                                if (reader[property.Name] != DBNull.Value)
                                {
                                    property.SetValue(entity, reader[property.Name]);
                                }
                            }
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        public T GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string typeName = typeof(T).Name;
                string procedureName = $"usp_Get{typeName}ById";
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            T entity = Activator.CreateInstance<T>();
                            foreach (var property in typeof(T).GetProperties())
                            {
                                if (reader[property.Name] != DBNull.Value)
                                {
                                    property.SetValue(entity, reader[property.Name]);
                                }
                            }
                            return entity;
                        }
                    }
                }
            }
            return null;
        }

        public T Insert(T entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string typeName = typeof(T).Name;
                string procedureName = $"usp_Inserir{typeName}";
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var property in typeof(T).GetProperties())
                    {
                        if (property.Name != "Id")
                        {
                            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
                        }
                    } 
                    cmd.Parameters.Add("@InsertedId", SqlDbType.Int).Direction = ParameterDirection.Output; // Parâmetro de saída para o ID inserido
                    connection.Open();
                    cmd.ExecuteNonQuery();

                    // Recupera o ID inserido
                    int insertedId = Convert.ToInt32(cmd.Parameters["@InsertedId"].Value);
                    // Atualiza a propriedade "Id" do objeto passado como parâmetro
                    typeof(T).GetProperty("Id").SetValue(entity, insertedId); 
                }
            } 
            return entity;
        }

        public void Update(T entity)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string typeName = typeof(T).Name;
                string procedureName = $"usp_Atualizar{typeName}";
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    foreach (var property in typeof(T).GetProperties())
                    {
                        cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
                    }
                    connection.Open();
                    cmd.ExecuteNonQuery();
                     
                }
            }  
        }

        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string typeName = typeof(T).Name;
                string procedureName = $"usp_Deletar{typeName}";
                using (SqlCommand cmd = new SqlCommand(procedureName, connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

}
