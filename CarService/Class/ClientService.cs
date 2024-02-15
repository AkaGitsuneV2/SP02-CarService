using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService.Class
{
    public class ClientService
    {
        public static List<string> GetListService(string id)
        {
			try
			{
				string s = "";
				var u = new List<string>();
				DBConnection.command.CommandText = $@"select service.Title,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения' from service, client, clientservice where 
													client.ID = clientservice.ClientID and clientservice.ServiceID = service.ID
													and client.id = {id}";
				DBConnection.reader = DBConnection.command.ExecuteReader();
				if (DBConnection.reader.HasRows)
				{
					while (DBConnection.reader.Read())
					{
						object title = DBConnection.reader["Title"];
						object countVisit = DBConnection.reader["Дата последнего посещения"];
						s = title.ToString() + ";" + countVisit + ";";
						u.Add(s);
					}
				}
				DBConnection.reader.Close();
				return u;

			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message);
				return null;
			}
        }
    }
}
