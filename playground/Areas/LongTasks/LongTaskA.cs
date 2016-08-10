using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;

namespace Playground.Areas.LongTasks
{
    public class LongTaskA
    {
        public int count;
        private CancellationToken cancelRequestToken;

        public LongTaskA(CancellationToken token)
        {
            cancelRequestToken = token;
            count = 0;
        }

        public void RunForSeconds(int seconds)
        {
            for(int i=0; i<seconds; i++)
            {
                Thread.Sleep(1000);
                if (cancelRequestToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }
    }
}