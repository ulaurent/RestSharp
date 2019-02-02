using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RestSharpPro
{
    class Program
    {
        static void Main(string[] args)
        {

            var client = new RestClient("https://jsonplaceholder.typicode.com/");

            // Get data from API and store in variable request ("resource/{id})", Method.POST)
            // This is of type "RestRequest"
            var request = new RestRequest("todos", Method.GET);

            List<Item> breakdown = new List<Item>();

            // Now we want to query the request
            // Client.Execute takes the request.data --> converts to a list of strings
            var queryResult = client.Execute<List<string>>(request).Data;

            foreach (string i in queryResult)
            {
                Item save = JsonConvert.DeserializeObject<Item>(i);
                breakdown.Add(save);
                Console.WriteLine(save.id);
            }

            // Now Query SQL Database using MYSQL and breakdown list of items
            // Establish connection to SQL Database


            Console.ReadLine();

            // Now set up connection to database & Inset data
            SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-CS7KDGHP\BABOSQL;Initial Catalog=RestSharp;User ID=sa;Password=***********");

            connection.Open();

            foreach (Item data in breakdown)
            {
                string send = string.Format("INSERT INTO Rest_Sharp (userId, LASTPRICE, CHANGE, DATETIME) VALUES ('{0}','{1}',{2},'{3}')", data.userId, data.id, data.title, data.completed);
                SqlCommand cmd = new SqlCommand(send, connection);

                cmd.ExecuteNonQuery();
            }

            connection.Close();

        }
    }
}
