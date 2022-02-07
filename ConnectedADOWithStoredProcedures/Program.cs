using System;
using System.Data.SqlClient;
using ConnectedADOWithStoredProcedures.Models;

namespace ConnectedADOWithStoredProcedures
{
    class Program
    {
        static SqlConnection connection;
        static SqlCommand command;
        // CRUD 
        // C > Create
        // R > Read

        // U > Update
        // D > Delete

        static string connectionString = "data source=LAPTOP-53S2KQS8;initial catalog=CGIDb;integrated security=true";
        static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        static void Main(string[] args)
        {
            Article article = new Article()
            { Title = "title3", Body = "Body of Title2 ", PublishDate = DateTime.Parse("12/12/2021") };

            //InsertArticle(article);
            Article article1 = GetArticleDetailsById(2);
            if(article1!=null)
            {
                Console.WriteLine(article1.Title  + " " + article1.Body + " " + article1.PublishDate);
            }
            DeleteArticle(2);

        }
        //static void InsertArticle(string title, string body,DateTime publishDate)
        //{
        static void InsertArticle(Article article)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand("InsertArticle"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;

                    command.Parameters.AddWithValue("@title", article.Title);
                    command.Parameters.AddWithValue("@body", article.Body);
                    command.Parameters.AddWithValue("@publishDate", article.PublishDate);
                    SqlParameter para1 = new SqlParameter();
                    para1.ParameterName = "@flag";
                    para1.Direction = System.Data.ParameterDirection.ReturnValue;
                   // para1.ParameterName = para1;
                    para1.SqlDbType = System.Data.SqlDbType.Int;
                    command.Parameters.Add(para1);
                    connection.Open();
                    command.ExecuteNonQuery();
                    int flag = (int)command.Parameters["@flag"].Value;
                    connection.Close();
                    if (flag ==0 )
                    { Console.WriteLine("Record with this title already exist");
                    }
                    else
                        Console.WriteLine("Record inserted");

                   
                }
            }
                //command.Dispose();
            //connection.Dispose();

        }

        static void UpdateArticle(int articleId, Article article)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand("UpdateArticle"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Connection = connection;
                    //command.Parameters.AddWithValue("@")
                }
            }
        }
        static void DeleteArticle(int articleId)
        {
            using (connection = GetConnection())
            {
                using (command = new SqlCommand("DeleteArticle"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@articleId", articleId);
                    command.Connection = connection;
                    connection.Open();
                    
                    int count = command.ExecuteNonQuery();
                    connection.Close();
                    if(count > 0)
                    {
                        Console.WriteLine("{0} records deleted", count); ;
                    }
                }
            }
        }
        static Article GetArticleDetailsById(int articleId)
        {
            using (connection = GetConnection())
            {
               
                using (command = new SqlCommand("GetArticleDetailsById"))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@articleId", articleId);

                    SqlParameter para1 = new SqlParameter();
                    para1.Direction = System.Data.ParameterDirection.Output;
                    para1.ParameterName = "@title";
                    para1.SqlDbType = System.Data.SqlDbType.VarChar;
                    para1.Size = 20;

                    SqlParameter para2 = new SqlParameter();
                    para2.Direction = System.Data.ParameterDirection.Output;
                    para2.ParameterName = "@body";
                    para2.SqlDbType = System.Data.SqlDbType.VarChar;
                    para2.Size = 8000;

                    SqlParameter para3 = new SqlParameter();
                    para3.Direction = System.Data.ParameterDirection.Output;
                    para3.ParameterName = "@publishedDate";
                    para3.SqlDbType = System.Data.SqlDbType.DateTime;

                    command.Parameters.Add(para1);
                    command.Parameters.Add(para2);
                    command.Parameters.Add(para3);
                    command.Connection = connection;
                    connection.Open();
                    command.ExecuteNonQuery();

                    Article article = new Article();
                    article.Body = command.Parameters["@body"].Value.ToString();
                    article.Title = command.Parameters["@title"].Value.ToString();
                    article.PublishDate = Convert.ToDateTime(command.Parameters["@publishedDate"].Value);
                     
                    connection.Close();
                    return article;




                }
            }
        }
    }
}
