using System.Collections.Generic;
using System.Linq;

namespace NSBManager.UserInterface.PhysicalModule.Model
{
    public static class PhysicalModelExtensions
    {
        public static IEnumerable<Server> Servers(this IPhysicalModel physicalModel)
        {
            var groupedServers = physicalModel.Endpoints.GroupBy(x => x.ServerName);

            foreach (var server in groupedServers)
            {
                yield return new Server {Name = server.Key};
            }
        }

        //Note: GuiEndpoint? 
        public static IEnumerable<GuiEndpoint> EndpointsOnServer(this IPhysicalModel physicalModel, string serverName)
        {
            var endpoints = physicalModel.Endpoints.Where(x => x.ServerName == serverName);

            //Todo: AutoMapper?
            foreach (var endpoint in endpoints)
            {
                yield return new GuiEndpoint { Id = endpoint.Id };
            }
        }
    }
}