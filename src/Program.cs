using System;
using System.Data.SQLite;

namespace CSharpConsoleApplication {
	class Program {
		static void Main(string[] args) {
			using (SQLiteConnection conn = new SQLiteConnection("URI=file:D:\\Projects\\Personal\\c-sharp-console-application\\Cookies")) {
				Console.WriteLine("a");

				int i = 0;

				conn.Open();

				Console.WriteLine("b");

				var command = conn.CreateCommand();

				command.CommandText = "SELECT name, encrypted_value FROM cookies;";

				var reader = command.ExecuteReader();

				Console.WriteLine("c");

				while (reader.Read()) {
					
					var encryptedData = (byte[])reader[1];
					var decodedData = System.Security.Cryptography.ProtectedData.Unprotect(encryptedData, null, System.Security.Cryptography.DataProtectionScope.CurrentUser);
					var plainText = System.Text.Encoding.ASCII.GetString(decodedData); // Looks like ASCII
					var clickId = reader.GetString(0);

					Console.WriteLine(reader.GetString(0) + /*" " + reader.GetString(1) + */ " " + plainText);
					++i;
				}

				Console.WriteLine("d");

				conn.Close();

				Console.WriteLine("eeee" + i.ToString());
			}
			Console.WriteLine("Hello World");
		}
	}
}
