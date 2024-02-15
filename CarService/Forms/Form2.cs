using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Components;
using MetroFramework.Animation;
using CarService.Class;
using System.Threading;

namespace CarService.Forms
{
    public partial class Form2 : MetroFramework.Forms.MetroForm
    {
        public Form2()
        {
            InitializeComponent();
        }
        
        public void DisplayLabelList(string id, string count)
        {
            List<string> serviceList = ClientService.GetListService(id);
            
            if (serviceList != null && serviceList.Count > 0)
            {
                StringBuilder resultText = new StringBuilder();

                foreach (string service in serviceList)
                {
                    string[] infoParts = service.Split(';');
                    string title = infoParts[0];
                    string lastVisitDate = infoParts[1];

                    resultText.Append($"Услуга: {title}" + "\n" + $"Последнее посещение: {lastVisitDate}" + "\r\n\r\n");
                    resultText.Append('\n' + $"Всего обращений: {count}");
                }
                label1.Text = resultText.ToString();
            }
            else
            {
                label1.Text = "Нет данных о услугах для данного клиента.";
            }
        }
    }
}
