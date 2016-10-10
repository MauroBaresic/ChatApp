using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using Business;
using Model;

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            //using (SqlConnection conn = new SqlConnection())
            //{
            //    conn.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ChatAppConnectionString"].ConnectionString;

            //    try
            //    {
            //        conn.Open();
            //        var cmd = "Select UserId, UserName from Users";
            //        using (SqlCommand sqlCommand = new SqlCommand(cmd, conn))
            //        {
            //            SqlDataReader reader = sqlCommand.ExecuteReader();
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {
            //                    Console.WriteLine("{0}\t{1}", reader.GetInt32(0),
            //                        reader.GetString(1));
            //                }
            //            }
            //            reader.Close();
            //        }

            //        using (SqlCommand sqlCommand = new SqlCommand())
            //        {
            //            sqlCommand.CommandType = CommandType.StoredProcedure;
            //            sqlCommand.CommandText = "AllUsers";
            //            sqlCommand.Connection = conn;

            //            SqlDataReader reader = sqlCommand.ExecuteReader();
            //            if (reader.HasRows)
            //            {
            //                while (reader.Read())
            //                {
            //                    Console.WriteLine("{0}", reader.GetString(0));
            //                }
            //            }
            //            reader.Close();
            //        }

            //        conn.Close();
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            using (Model.ChatAppDBEntities context = new ChatAppDBEntities())
            {
                var users = context.Users.ToList();
                foreach (User user in users)
                {
                    Console.WriteLine(user.UserId + "\t" + user.UserName);
                }

                var result = context.AllUsers();
                foreach (string username in result)
                {
                    Console.WriteLine(username);
                }
            }



            using (ServiceHost host = new ServiceHost(typeof(ChatService)))
            {
                host.Open();

                var address = "";//
                Console.WriteLine($"Up and running on {address}");

                while (true)
                {
                    Console.Write("Enter q to quit: ");
                    var input = Console.ReadLine();
                    if (input?.ToLower() == "q") break;
                }

                Console.WriteLine("Please wait, closing ...");
                host.Close();
            }
        }
    }
}
