using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace ServiceTest
{
    public partial class Service1 : ServiceBase

    {
        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(System.IntPtr handle, ref ServiceStatus serviceStatus);

        public enum ServiceState
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ServiceStatus
        {
            public int dwServiceType;
            public ServiceState dwCurrentState;
            public int dwControlsAccepted;
            public int dwWin32ExitCode;
            public int dwServiceSpecificExitCode;
            public int dwCheckPoint;
            public int dwWaitHint;
        };

        private int eventId = 1;

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            // TODO: Insert monitoring activities here.
            eventLog1.WriteEntry("Monitoring the System", EventLogEntryType.Information, eventId++);

            eventLog1.WriteEntry("le programme entre dans timer1");

            //Connexion à la bdd et création du curseur:
            AccessBdd crs = new AccessBdd();

            //On vérifie qu'on est bien entre le 1 et le 10 du mois:
            if (GestionDates.entre(1, 10) == true)
            {
                //Récupération des fiches du mois précédent et maj de celles-ci:
                //Récupération du mois précédent et son année
                String moisPrecedent = GestionDates.getMoisPrecedent();
                String annee = GestionDates.getAnnee(DateTime.Today);
                string mois = annee + moisPrecedent;

                crs.reqUpdate("update fichefrais set idetat='CL' where mois = " + mois + " and idetat='CR'");

            }
            //Si on est après le 20 du mois:
            if (GestionDates.entre(20, 31) == true)
            {
                //Récupération des fiches du mois précédent et maj de celles-ci:
                String moisPrecedent = GestionDates.getMoisPrecedent();
                String annee = GestionDates.getAnnee(DateTime.Today);
                string mois = annee + moisPrecedent;

                crs.reqUpdate("update fichefrais set idetat='RB' where mois = " + mois + " and idetat='VA'");
            }
            }

        public Service1()
        {

            InitializeComponent();

            eventLog1 = new System.Diagnostics.EventLog();
            if (!System.Diagnostics.EventLog.SourceExists("MySource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "MySource", "MyNewLog");
            }
            eventLog1.Source = "MySource";
            eventLog1.Log = "MyNewLog";

        }

        protected override void OnStart(string[] args)
        {
            // Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);


            eventLog1.WriteEntry("In OnStart");
            // Set up a timer that triggers every minute.
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 60000; // 60 seconds
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();


            // Update the service state to Running.
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        protected override void OnStop()
        {
            eventLog1.WriteEntry("In OnStop.");
        }
    }
}
