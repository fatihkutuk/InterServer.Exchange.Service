using Dapper;
using InterServer.Exchange.Service.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterServer.Exchange.Service.Manager
{
    public class DatabaseManager
    {
        String connectionString;
        public DatabaseManager()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DbStr"].ConnectionString;

            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<Server>> GetServerList()
        {

            try
            {
                using (var con = new MySqlConnection(connectionString))
                {
                    return con.QueryAsync<Server>("select * from servers").Result.ToList();
                }
            }
            catch (Exception)
            {

               throw;
            }

        }
        public async Task<List<CloneTag>> GetCloneTags()
        {
            try
            {
                using (var con = new MySqlConnection(connectionString))
                {
                    return con.QueryAsync<CloneTag>("select * from clonetags").Result.ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }


        }
        public async Task CloneTag(CloneTag CloneTag)
        {
            try
            {
                Tag tag;
                using (var con = new MySqlConnection(CloneTag.SrcServer.GetConnectionString().Result))
                {
                    tag = con.QueryAsync<Tag>($"select * from _tagoku where devId={CloneTag.srcDeviceId} and tagName= '{CloneTag.srcTagName}'").Result.FirstOrDefault();
                }
                if (tag != null)
                {
                    using (var con = new MySqlConnection(CloneTag.DestServer.GetConnectionString().Result))
                    {
                        try
                        {
                            await con.ExecuteAsync($"replace into _tagoku (devId, tagName, tagValue, readTime)  " +
                                 $"VALUES ({CloneTag.destDeviceId}, '{CloneTag.SrcServer.serverKey}-{CloneTag.srcDeviceId}-{CloneTag.srcTagName}', {Convert.ToDouble(tag.tagValue).ToString("f6", CultureInfo.InvariantCulture)}, '{tag.readTime.ToString("yyyy-MM-dd HH::mm:ss")}');");
                        }
                        catch (Exception ex)
                        {

                            throw;
                        }

                    }
                }
                else
                {
                    CloneTag.enable = false;
                }

                CloneTag.lastTime = DateTime.Now;
                tag = null;
            }
            catch (Exception)
            {

                throw;
            }

        }
        public async Task<List<Tag>> GetTagOkuTags( string config)
        {

            using (var con = new MySqlConnection(config))
            {
                try
                {

                    return con.QueryAsync<Tag>("select * from _tagoku ").Result.ToList();

                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
