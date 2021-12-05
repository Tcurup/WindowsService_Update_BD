using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Timers;



namespace WindowsServiceBD02
{
    public partial class Service1 : ServiceBase
    {
        static string conexionstring = "server=PC-INFORMATICA0; database=Base_Datos_Tulio; integrated security=true";
        SqlConnection conexion = new SqlConnection(conexionstring);
        
        public Service1()
        {
            InitializeComponent();

        
            if (!System.Diagnostics.EventLog.SourceExists("MySourse")){
                
                
                System.Diagnostics.EventLog.CreateEventSource("MySourse", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";
            
            
        }
        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("Iniciado!!");
            conexion.Open();
            // Set up a timer to trigger every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();


        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("Terminado!!");
           
        }

        public void OnTimer(object sender, ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information);
            string cadena = "update tbl_Registros set Control1 = '1' where Control1 = '0'";
            SqlCommand comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();

        }

        private void eventLog1_EntryWritten(object sender, EntryWrittenEventArgs e)
        {

        }

    }
}
