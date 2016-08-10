using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Diagnostics;

namespace Playground.Areas.LongTasks.Controllers
{
    public class DefaultController : Controller
    {
        static int CountRunningTasks = 0;

        // GET: LongTasks/Default/Index
        public ActionResult Index()
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("Info", "Hello World!");
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: LongTasks/Default/StartTask
        public ActionResult StartTask()
        {
            Dictionary<string, object> json = new Dictionary<string, object>();

            HostingEnvironment.QueueBackgroundWorkItem(cancelRequest =>
            {
                Debug.WriteLine("About to startup a new task");
                CountRunningTasks++;
                var obj = new LongTaskA(cancelRequest);
                obj.RunForSeconds(35);
                CountRunningTasks--;
                Debug.WriteLine("About to kill a task");
            });

            json.Add("Info", "Task Started");
            json.Add("taskid", 1234);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: LongTasks/Default/CheckTasks
        public ActionResult CheckTasks()
        {
            Dictionary<string, object> json = new Dictionary<string, object>();
            json.Add("Info", "Check Current Running Tasks");
            json.Add("RunningTasks", CountRunningTasks);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}