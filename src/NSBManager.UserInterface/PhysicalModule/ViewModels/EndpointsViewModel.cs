using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.PresentationFramework.Screens;
using Caliburn.PresentationFramework;
using NSBManager.UserInterface.PhysicalModule.Model;

namespace NSBManager.UserInterface.PhysicalModule.ViewModels
{
    public class EndpointsViewModel : Screen
    {
        public IObservableCollection<GuiEndpoint> Endpoints { get; set; }

        private readonly IPhysicalModel physicalModel;

        public EndpointsViewModel(IPhysicalModel physicalModel)
        {
            this.physicalModel = physicalModel;

            Endpoints = new BindableCollection<GuiEndpoint>(ConvertEndPoints());
        }

        private IEnumerable<GuiEndpoint> ConvertEndPoints()
        {
            //TODO: Automapper?
            foreach (var endpoint in physicalModel.Endpoints)
            {
                yield return new GuiEndpoint
                {
                    Id = endpoint.Id,
                    Name = endpoint.Id,
                    HostType = endpoint.HostType,
                    Status = endpoint.Status.ToString()
                };
            }
        }
    }
}
