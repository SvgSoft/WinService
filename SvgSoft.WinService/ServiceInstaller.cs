using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Collections;

namespace SvgSoft.WinService
{
    [RunInstaller(true)]
    public class ServiceInstaller : Installer
    {
        public string ServiceName { get; set; }
        public string FileName { get; protected set; }
        public bool RestartOnFailure { get; set; }
        public int RestartDelay { get; set; }
        public System.Collections.Specialized.StringDictionary Parameters { get; protected set; }
        public ServiceInstaller() : base()
        {
            ServiceName = "Service1";
            RestartOnFailure = true;
            RestartDelay = 30000;
        }        
        private void initParameters()
        {
            // определяем путь к исполняемому файлу службы
            if (Context == null || Context.Parameters == null) Parameters = new System.Collections.Specialized.StringDictionary();
            else Parameters = this.Context.Parameters;
            string parName = "assemblypath";
            if (Parameters.ContainsKey(parName)) FileName = Parameters[parName];
        }
        protected override void OnBeforeInstall(IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);
            initParameters();
            debugParameters();
            // перед установкой останавливаем и удаляем службу, если она существует
            if (Service.Exist(ServiceName))
            {
                string error;
                bool stop = Service.Stop(ServiceName, out error);
                debug(string.Format("Stop Service \"{0}\": {1}. {2}", ServiceName, stop, error));
                bool uninstall = Service.Uninstall(ServiceName, out error);
                debug(string.Format("Uninstall Service \"{0}\": {1}. {2}", ServiceName, uninstall, error));
            }
        }
        protected override void OnCommitting(IDictionary savedState)
        {
            base.OnCommitting(savedState);
            initParameters();
        }
        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);
            debugParameters();

            // устанавливаем, настраиваем и запускаем
            string error = null;
            bool install = Service.Install(FileName, ServiceName, out error, uninstallIfExist: true);
            debug(string.Format("Install Service \"{0}\": {1}. {2}", ServiceName, install, error));
            if (!install)
                throw new Exception(string.Format("Не удалось установить службу {0} ({1})", ServiceName, error));

            if(RestartOnFailure)
                Service.SetRestartOnFailure(ServiceName, RestartDelay); // настраиваем действия при сбое

            bool start = Service.Start(ServiceName, out error);
            debug(string.Format("Start Service \"{0}\": {1}. {2}", ServiceName, start, error));
            if (!start)
                throw new Exception(string.Format("Не удалось запустить службу {0} ({1})", ServiceName, error));
        }
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            initParameters();
            debugParameters();
            // перед удалением останавливаем и удаляем службу
            string error = null;
            bool stop = Service.Stop(ServiceName, out error);
            debug(string.Format("Stop Service \"{0}\": {1}. {2}", ServiceName, stop, error));
            bool uninstall = Service.Uninstall(ServiceName, out error);
            debug(string.Format("Uninstall Service \"{0}\": {1}. {2}", ServiceName, uninstall, error));
            string message = "Uninstall";
            foreach (string dir in Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory))
                message += string.Format("{0}\\\r\n", dir);
            foreach (string file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory))
                message += string.Format("{0}\r\n", file);
            debug(message);
            base.OnBeforeUninstall(savedState);
        }
        protected override void OnBeforeRollback(IDictionary savedState)
        {
            base.OnBeforeRollback(savedState);
            initParameters();
        }
        protected void debugParameters()
        {
#if DEBUG
            string message = "ServiceInstaller debug info:\r\n";
            InstallContext context = Context;
            if (context == null)
                message += "Context is null";
            else if (context.Parameters == null)
                message += "Context.Parameters is null";
            else
            {
                foreach (string key in context.Parameters.Keys)
                {
                    message += string.Format("{0} = {1}\r\n", key, context.Parameters[key]);
                }
            }
            debug(message);
#endif
        }

        protected void debug(string message)
        {
#if DEBUG
            try
            {
                EventLog.WriteEntry("MsiInstaller", message, EventLogEntryType.Information);
            }
            catch { }
#endif
        }
    }
}