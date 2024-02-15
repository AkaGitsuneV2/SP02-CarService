using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Data;

namespace CarService.Class
{
    public class ClassRefactorClients
    {
        static public DataTable dtTags = new DataTable();
        public static int InsertClient(string firstName, string lastName, string patronymic, string email, string phone, string birthday, string registrationDate, string gender, string photo)
        {
			try
			{
				
				DBConnection.command.CommandText = $@"INSERT INTO client VALUES(null, '{firstName}','{lastName}', '{patronymic}', '{birthday}', '{registrationDate}', '{email}', {phone}, '{gender}','{photo}')";
				DBConnection.command.ExecuteScalar();

				return 1;
			}
			catch (Exception e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message,"Ошибка",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				return 0;
			}
        }
		public static int EditClient(string id, string firstName, string lastName, string patronymic, string email, string phone, string birthday, string gender, string photo)
		{
			try
			{
				DBConnection.command.CommandText = $@"UPDATE client SET FirstName = '{firstName}', LastName = '{lastName}', Patronymic = '{patronymic}', Birthday = '{birthday}', Email = '{email}',Phone = '{phone}', GenderCode = '{gender}', PhotoPath = '{photo}' WHERE ID = {id}";
				DBConnection.command.ExecuteScalar();
				return 1;
			}
			catch (Exception e)
			{
                System.Windows.Forms.MessageBox.Show(e.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return 0;
			}
		}

		public static bool DeleteClient(string id)
        {
            try
			{
                DBConnection.command.CommandText = $"SELECT COUNT(*) FROM tagofclient WHERE ClientID = {id}";
                int tagCount = Convert.ToInt32(DBConnection.command.ExecuteScalar());

                DBConnection.command.CommandText = $"SELECT COUNT(*) FROM clientservice WHERE ClientID = {id}";
                int visitCount = Convert.ToInt32(DBConnection.command.ExecuteScalar());

                if (visitCount > 0)
                {
                    // Если у клиента есть посещения, выводим сообщение и не удаляем
                    System.Windows.Forms.MessageBox.Show("Нельзя удалить клиента с информацией о посещениях.", "Предупреждение", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return false;
                }

                // Если у клиента есть теги, удаляем их
                if (tagCount > 0)
                {
                    DBConnection.command.CommandText = $"DELETE FROM tagofclient WHERE ClientID = {id}";
                    DBConnection.command.ExecuteNonQuery();
                }

                // Удаляем самого клиента
                DBConnection.command.CommandText = $"DELETE FROM client WHERE ID = {id}";
                DBConnection.command.ExecuteNonQuery();

                System.Windows.Forms.MessageBox.Show("Клиент успешно удален.", "Успех", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
                return true;
            }
			catch (Exception e)
			{
                System.Windows.Forms.MessageBox.Show(e.Message, "Ошибка", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return false;
            }
		}

        #region Добавление и удаление тега клиента
        public static void GetTagTable()
        {
            DBConnection.command.CommandText = "SELECT * FROM auto_service_task_4.tag;";
            dtTags.Clear();
            DBConnection.adapter.Fill(dtTags);
        }

        public static bool AppendTag(string id, string tag)
        {
            try
            {
                DBConnection.command.CommandText = $@"SELECT COUNT(*) FROM tagofclient WHERE ClientID = {id} AND TagID = '{tag}'";
                Object r = DBConnection.command.ExecuteScalar();
                int count = Convert.ToInt32(r);
                if (count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Тег уже прикреплен");
                    return false;
                }
                else
                {
                    DBConnection.command.CommandText = $@"insert into tagofclient values('{id}','{tag}')";
                    DBConnection.command.ExecuteScalar();
                    return true;
                }

            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
        }
        public static bool DeleteTagClient(string id, string tag)
        {
            try
            {
                DBConnection.command.CommandText = $@"delete from tagofclient where ClientID = '{id}' and TagID = '{tag}'";
                DBConnection.command.ExecuteScalar();
                return true;
            }
            catch (Exception e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message);
                return false;
            }
        }
        #endregion
    }
}
