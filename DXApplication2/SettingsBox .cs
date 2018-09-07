﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DXApplication2.Properties;

using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Entity.Core.EntityClient;

namespace DXApplication2
{
    public partial class SettingsBox : DevExpress.XtraEditors.XtraUserControl
    {

        private SqlConnectionStringBuilder sd;
        private string servidoro;




        public SettingsBox()
        {
            InitializeComponent();
            //InitTextboxes();
        }

        private void UpdateConfig(string value)
        {
            //var configFile = ConfigurationManager.OpenExeConfiguration(fileName);
            //configFile.ConnectionStrings[key].Value = value;

            //configFile.Save();


            // Build the MetaData... feel free to copy/paste it from the connection string in the config file.
            EntityConnectionStringBuilder esb = new EntityConnectionStringBuilder();
            esb.Metadata = "res://*/DBData.csdl|res://*/DBData.ssdl|res://*/DBData.msl";
            esb.Provider = "System.Data.SqlClient";
            esb.ProviderConnectionString = value;

            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings["ContabilidadEntities"].ConnectionString = esb.ConnectionString;
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        private void groupControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SettingsBox_Load(object sender, EventArgs e)
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dataTab = instance.GetDataSources();
            int cant = dataTab.Rows.Count;
            var serverlist = new object[cant];
            var dbaselist = new object[cant];
            for (int i = 0; i < cant; i++)
            {
                object[] arr = dataTab.Rows[i].ItemArray;
                if (arr[1] == null)
                {
                    serverlist[i] = (object)((string)arr[0] + "\\");
                }
                else
                {
                    serverlist[i] = (object)((string)arr[0] + "\\" + arr[1].ToString());
                }

                dbaselist[i] = arr[2];
            }

            int w = serverlist.Length;

            var element = new string[w];
            for (int i = 0; i < w; i++)
            {
                element[i] = serverlist[i].ToString();
            }
            SerList.Properties.Items.AddRange(element);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                //  UpdateConfig("ContabilidadEntities", BuildConnectionString(SerList.Text, "Contabilidad"), "C:\\Users\\ivan\\Documents\\Visual Studio 2015\\Projects\\DXApplication2\\DXApplication2\\bin\\Debug\\DXApplication2.exe");
                sd = new SqlConnectionStringBuilder();
                if (SerList.Text == " ")
                {
                    sd.DataSource = (string)SerList.SelectedItem;
                }
                else
                {
                    sd.DataSource = (string)SerList.Text;
                }
                servidoro = sd.DataSource;

                if (IsSQLUser.Checked == true)
                {
                    sd.IntegratedSecurity = false;
                    sd.UserID = SQLUserEdit.Text;
                    sd.Password = PassEdit.Text;
                }
                else
                {
                    sd.IntegratedSecurity = true;
                }
                sd.InitialCatalog = "Contabilidad";

                var sdf = new SqlConnection(sd.ConnectionString);
                UpdateConfig(sd.ConnectionString);

                //Settings.Default.Save();


                //WriteDataToFile();
            }
            catch (Exception)
            {
                MessageBox.Show("error de config");
            }
                 //Settings.Default.ContabilidadConnectionString
        }
    }
}
