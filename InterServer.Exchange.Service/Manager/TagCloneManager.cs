using InterServer.Exchange.Service.Model;
using K4os.Hash.xxHash;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterServer.Exchange.Service.Manager
{
    public class TagCloneManager
    {
        DatabaseManager databaseManager;
        List<Server> servers;
        List<CloneTag> cloneTags;
        
        public TagCloneManager()
        {
            databaseManager = new DatabaseManager();
            servers = new List<Server>();
            cloneTags = new List<CloneTag>();

        }
        public async Task Run()
        {
            servers = databaseManager.GetServerList().Result;
            cloneTags = databaseManager.GetCloneTags().Result;
            cloneTags.ForEach(async x => await x.SetServer(servers));
            //var s = DateTime.Now;
            foreach (var item in cloneTags.Where(x=>x.enable))
            {
                try
                {
                    if ((DateTime.Now - item.lastTime).TotalMilliseconds > item.interval)
                    {
                        await databaseManager.CloneTag(item);

                    }
                }
                catch (Exception ex)
                {

                }

            }
          //  var f = DateTime.Now;
            //var fark = (f-s).TotalMilliseconds;
            //Console.WriteLine("bitti");
        }
    }
}
