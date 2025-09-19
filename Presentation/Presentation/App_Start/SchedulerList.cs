using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.IO;
using System.Net.Http;

using Application.Presentation;
using Application.Common;

namespace Presentation
{
    public sealed class SchedulerList
    {
        static SchedulerList _instance;

        public static SchedulerList Instance
        {
            get { return _instance ?? (_instance = new SchedulerList()); }
        }

        private SchedulerList()
        { }

        private static System.Timers.Timer myTimerSchedulerMail;

        private static HttpContext theContext;
        public static DataSet GetSchedulerMailList(Int32 theStatus)
        {
            string theParameter = string.Format("?theStatus={0}", theStatus.ToString());
            string theResult = ObjectFactory.GetStringAsync("Administration/SystemUtility/GetSchedulerMailList", theParameter, "SystemUtilityController");
            DataSet theDS = clsDataOperations.ConvertToDataset(theResult);
            return theDS;
        }
        
        public static void StartSchedulerMail(HttpContext currentContext)
        {
            theContext = currentContext;

            myTimerSchedulerMail = new System.Timers.Timer();
            myTimerSchedulerMail.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["SchedulerMailInterval"].ToString());
            myTimerSchedulerMail.AutoReset = true;
            myTimerSchedulerMail.Elapsed += new System.Timers.ElapsedEventHandler(myTimerSchedulerMail_Elapsed);
            myTimerSchedulerMail.Enabled = true;
        }

        public static void myTimerSchedulerMail_Elapsed(object source, System.Timers.ElapsedEventArgs e)
        {
            myTimerSchedulerMail.Stop();

            try
            {
                
                clsMail mail = new clsMail();                
                DataTable dtMailList = GetSchedulerMailList(0).Tables[0];
                foreach (DataRow dr in dtMailList.Rows)
                {
                    if (!string.IsNullOrEmpty(dr["toMail"].ToString()))
                    {
                        mail.SchedulerSendMail(dr["Id"].ToString(), dr["toMail"].ToString(), dr["Subject"].ToString(), dr["Message"].ToString(), dr["AttachementFile"].ToString(), theContext, dr["EmailCategoryId"].ToString(), dr["OrganizationId"].ToString(), dr["RoleId"].ToString());
                    }
                }

                myTimerSchedulerMail.Start();
            }
            catch(Exception err)
            {
                myTimerSchedulerMail.Start();
            }
        }

        
    }
}