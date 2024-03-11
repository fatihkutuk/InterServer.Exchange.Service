using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterServer.Exchange.Service.Model
{
    public class Server 
    {
        public int id { get; set; }
        public string ServerTitle { get; set; }
        public string serverKey { get; set; }
        public string ipAdress { get; set; }
        public string ipAdressFkm { get; set; }
        public string domain { get; set; }
        public string dbuser { get; set; }
        public string dbpassword { get; set; }
        public int dbport { get; set; }
        public List<Tag> tagOku { get; set; }
        public DateTime createdDate { get; set; }
        public DateTime updatedDate { get; set; }

        public string DbConfig { get; set; }
        public async Task<String> GetConnectionString()
        {
            return $"Server={this.ipAdress};Port={this.dbport};Database=kbindb;Uid={this.dbuser};password={this.dbpassword};SslMode=none;convert zero datetime=True;";
        }
        public async Task FillTags(List<Tag> tags)
        {
            tagOku = tags;
        }
    }
}
