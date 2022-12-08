using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateNameDetails
{
	class Program
	{
		static void Main(string[] args)
		{
			//connect to database
			string connectionString = @"Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=NameDatabase;Integrated Security=True";
			SqlConnection connection = new SqlConnection(connectionString);
			connection.Open();

			//open the csv file
			string nameDetailFilename = @"E:\WithoutHaste\www\dbNames\backupTableDetail.csv";
			using(StreamReader reader = new StreamReader(nameDetailFilename))
			{
				string line = null;
				int lineCount = 0;
				while((line = reader.ReadLine()) != null)
				{
					string[] fields = line.Split(',');
					int id = Int32.Parse(RemoveQuotes(fields[0]));
					bool isBoy = (RemoveQuotes(fields[3]) == "1");
					bool isGirl = (RemoveQuotes(fields[4]) == "1");

					//update database
					string commandString = "update dbo.NameDetail set IsBoy = @IsBoy, IsGirl = @IsGirl where Id = @Id";
					using(SqlCommand command = new SqlCommand(commandString, connection))
					{
						SqlParameter isBoyParameter = new SqlParameter("IsBoy", isBoy);
						SqlParameter isGirlParameter = new SqlParameter("IsGirl", isGirl);
						SqlParameter idParameter = new SqlParameter("Id", id);

						command.Parameters.Add(isBoyParameter);
						command.Parameters.Add(isGirlParameter);
						command.Parameters.Add(idParameter);

						command.ExecuteNonQuery();
					}

					if(lineCount % 100 == 0)
						Console.WriteLine("Line {0}", lineCount);
					lineCount++;
				}
			}

			//close connection to database
			connection.Close();

			Console.WriteLine("\nDone");
			Console.ReadLine();
		}

		private static string RemoveQuotes(string text)
		{
			return text.Replace('"', ' ').Trim();
		}
	}
}
