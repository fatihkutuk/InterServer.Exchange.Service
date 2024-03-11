using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterServer.Exchange.Service.Model
{
    public class CloneTag
    {
        public int id { get; set; }
        public int srcServerId { get; set; }
        public int srcDeviceId { get; set; }
        public string srcTagName { get; set; }
        public int destServerId { get; set; }
        public int destDeviceId { get; set; }
        public string destTagName { get; set; }
        public int interval { get; set; }
        public Server SrcServer { get; set; }
        public Server DestServer { get; set; }
        public double value { get; set; } = 0;
        public DateTime lastTime { get; set; } = DateTime.Now.AddDays(-1);
        public bool enable { get; set; } = true;
        public async Task SetServer(List<Server> servers)
        {
            this.SrcServer = servers.Where(x => x.id == srcServerId).FirstOrDefault();
            this.DestServer = servers.Where(x => x.id == destServerId).FirstOrDefault();

        }
    }
}
