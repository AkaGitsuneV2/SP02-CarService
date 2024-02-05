using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService.Class
{
	public class ClientsClass
	{
        #region Вывод данных в таблицу. Вывод по условию 10,50,200,все
        public static void GetTableClient(int startIndex, int pageSize)
		{
			try
			{
				DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений', 
													(SELECT TagID FROM tagofclient WHERE client.ID = tagofclient.ClientID) AS 'Тег' FROM client LIMIT {startIndex}, {pageSize}";
				DBConnection.dtClients.Clear();
				DBConnection.adapter.Fill(DBConnection.dtClients);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			}
		}

		public static void GetFullTable()
		{
			try
			{
                DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений', 
													(SELECT TagID FROM tagofclient WHERE client.ID = tagofclient.ClientID) AS 'Тег' FROM client";
                DBConnection.dtClients.Clear();
                DBConnection.adapter.Fill(DBConnection.dtClients);
            }
			catch (Exception ex)
			{
                System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
		public static int GetTotalRecordsCount()
		{
			try
			{
				DBConnection.command.CommandText = "SELECT COUNT(*) FROM client";
				int totalRecords = Convert.ToInt32(DBConnection.command.ExecuteScalar());
				return totalRecords;
			}
			catch (Exception ex)
			{
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
		}

        #endregion

		public static void GetTableClientWithSearch(string firstName, string email, string phone, int pageSize, int offset)
		{
			try
			{
				StringBuilder sb = new StringBuilder(" WHERE 1=1");

				if (!string.IsNullOrEmpty(firstName))
					sb.Append(" AND CONCAT(FirstName, LastName, Patronymic) LIKE @firstName");

				if (!string.IsNullOrEmpty(email))
                    sb.Append(" AND Email LIKE @email");

				if (!string.IsNullOrEmpty(phone))
                    sb.Append(" AND Phone LIKE @phone");

				DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone, RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений', 
													(SELECT TagID FROM tagofclient WHERE client.ID = tagofclient.ClientID) AS 'Тег' FROM client {sb} LIMIT {offset} {pageSize}";
				DBConnection.command.Parameters.Clear();

                if (!string.IsNullOrEmpty(firstName))
                    DBConnection.command.Parameters.AddWithValue("@firstName", $"%{firstName}%");

                if (!string.IsNullOrEmpty(email))
                    DBConnection.command.Parameters.AddWithValue("@email", $"%{email}%");

                if (!string.IsNullOrEmpty(phone))
                    DBConnection.command.Parameters.AddWithValue("@phone", $"%{phone}%");

                DBConnection.dtClients.Clear();
                DBConnection.adapter.Fill(DBConnection.dtClients);
            }
			catch (Exception e)
			{
				MessageBox.Show(e.Message,"Ошибка",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
    }
}

