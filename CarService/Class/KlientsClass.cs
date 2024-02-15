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
        #region Работа с таблицей клиенты
        #region Вывод данных в таблицу. Вывод по условию 10,50,200,все
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="pageSize"></param>
        public static void GetTableClient(int startIndex, int pageSize)
		{
			try
			{
				DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений' FROM client LIMIT {startIndex}, {pageSize}";
				DBConnection.dtClients.Clear();
				DBConnection.adapter.Fill(DBConnection.dtClients);
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		public static void GetFullTable()
		{
			try
			{
                DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений' FROM client";
                DBConnection.dtClients.Clear();
                DBConnection.adapter.Fill(DBConnection.dtClients);
            }
			catch (Exception ex)
			{
                System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
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
		public static void GetGender()
		{
			try
			{
				DBConnection.command.CommandText = "SELECT * FROM gender";
				DBConnection.dtGender.Clear();
				DBConnection.adapter.Fill(DBConnection.dtGender);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		public static void GetTableClientWithSearch(string firstName, string email, string phone, string genderCode, int pageSize, int offset)
		{
            try
            {
                StringBuilder whereClause = new StringBuilder(" WHERE 1=1");

                if (!string.IsNullOrEmpty(firstName))
                    whereClause.Append(" AND CONCAT(FirstName, ' ', LastName) LIKE @firstName");

                if (!string.IsNullOrEmpty(email))
                    whereClause.Append(" AND Email LIKE @email");

                if (!string.IsNullOrEmpty(phone))
                    whereClause.Append(" AND Phone LIKE @phone");

                if (!string.IsNullOrEmpty(genderCode))  // Проверка, что пол выбран
                    whereClause.Append(" AND GenderCode = @genderCode");

                DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
                                                (SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
                                                (SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений' 
                                                FROM client 
                                                {whereClause}
                                                LIMIT @offset, @pageSize";

                DBConnection.command.Parameters.Clear();

                if (!string.IsNullOrEmpty(firstName))
                    DBConnection.command.Parameters.AddWithValue("@firstName", $"%{firstName}%");

                if (!string.IsNullOrEmpty(email))
                    DBConnection.command.Parameters.AddWithValue("@email", $"%{email}%");

                if (!string.IsNullOrEmpty(phone))
                    DBConnection.command.Parameters.AddWithValue("@phone", $"%{phone}%");

                if (!string.IsNullOrEmpty(genderCode))
                    DBConnection.command.Parameters.AddWithValue("@genderCode", genderCode);

                DBConnection.command.Parameters.AddWithValue("@offset", offset);
                DBConnection.command.Parameters.AddWithValue("@pageSize", pageSize);

                DBConnection.dtClients.Clear();
                DBConnection.adapter.Fill(DBConnection.dtClients);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
        public static void Sort(int i)
        {
			string sortColumn = "";
			string orderBy = "";
            switch (i)
			{
				case 1:
					sortColumn = "LastName";
					orderBy = "ASC";
					break;
				case 2:
					sortColumn = "(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID)";
                    orderBy = "DESC";
                    break;
				case 3:
					sortColumn = "(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID)";
					orderBy = "DESC";
					break;
            }
            DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений' FROM client order by {sortColumn} {orderBy} LIMIT 1000 ";
            DBConnection.dtClients.Clear();
            DBConnection.adapter.Fill(DBConnection.dtClients);
        }
		public static void HappyBirthday()
		{
			try
			{
				DBConnection.command.CommandText = $@"SELECT ID, GenderCode, LastName, FirstName, Patronymic, Birthday, Phone,RegistrationDate, Email,
													(SELECT max(StartTime) FROM clientservice where client.ID = clientservice.ClientID) as 'Дата последнего посещения',
													(SELECT count(*) FROM clientservice WHERE client.ID = clientservice.ClientID) as 'Количество посещений' FROM client where month(Birthday)  = month(curdate())";
                DBConnection.dtClients.Clear();
                DBConnection.adapter.Fill(DBConnection.dtClients);
            }
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
        #endregion
        #region Работа с тегами
        public static void GetTableTag(string id)
        {
			try
			{
				DBConnection.command.CommandText = $@"SELECT * FROM tag where ID IN (SELECT TagID FROM tagofclient WHERE ClientID = {id})";
				DBConnection.dtTag.Clear();
				DBConnection.adapter.Fill(DBConnection.dtTag);
			}
			catch (Exception ex)
			{

				MessageBox.Show(ex.Message);
			}
        }
        #endregion

		public static string GetPhotoPath(string id)
		{
			string result = "";
			DBConnection.command.CommandText = $@"select PhotoPath from client where ID = '{id}'";
			object r = DBConnection.command.ExecuteScalar();
			if (r != null)
			{
                result = r.ToString();
				return result;
            }
			return result;

        }
		public static string Count()
		{
			DBConnection.command.CommandText = "select count(*) from client";
			object r = DBConnection.command.ExecuteScalar();
			if (r != null)
			{
				return r.ToString();
			}
			return null;
		}
    }
}

