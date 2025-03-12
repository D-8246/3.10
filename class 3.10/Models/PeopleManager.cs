using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace class_3._10.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class PeopleManager
    {
        private string _connectionString;

        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;

        }

        public List<Person> GetPeople()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People";
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            List<Person> people = new List<Person>();
            while (reader.Read())
            {
                people.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],
                });
            }
            return people;
        }

        public void AddPerson(Person p)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO People VALUES(@first, @last, @age)";
            cmd.Parameters.AddWithValue("@first", p.FirstName);
            cmd.Parameters.AddWithValue("@last", p.LastName);
            cmd.Parameters.AddWithValue("@age", p.Age);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void DeletePerson(int id)
        {
            SqlConnection connection = new SqlConnection(Properties.Settings.Default.ConStr);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
        public Person GetPersonById(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM People WHERE Id = @id";
            cmd.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            return (new Person
            {
                Id = (int)reader["Id"],
                FirstName = (string)reader["FirstName"],
                LastName = (string)reader["LastName"],
                Age = (int)reader["Age"]
            });

        }

        public void Update(Person person)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age " +
                "WHERE Id = @id";
            cmd.Parameters.AddWithValue("@firstName", person.FirstName);
            cmd.Parameters.AddWithValue("@lastName", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@id", person.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}