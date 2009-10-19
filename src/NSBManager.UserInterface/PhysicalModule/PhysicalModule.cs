using System;
using Microsoft.Practices.Composite.Modularity;
using Microsoft.Practices.Composite.Regions;
using NSBManager.UserInterface.Infrastructure;
using NSBManager.UserInterface.PhysicalModule.Views;
using StructureMap;

namespace NSBManager.UserInterface.PhysicalModule
{
    public class PhysicalModule : IModule
    {
        private readonly IRegionManager regionManager;

        public PhysicalModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }

        public void Initialize()
        {
            RegisterViewsAndServices();

            IRegion mainRegion = regionManager.Regions[RegionNames.MainRegion];
            mainRegion.Add(ObjectFactory.GetInstance<IServerPresentationModel>().View);

            IRegion detailsRegion = regionManager.Regions[RegionNames.DetailsRegion];
            detailsRegion.Add(ObjectFactory.GetInstance<IServerDetailsPresentationModel>().View);

        }

        private void RegisterViewsAndServices()
        {
            ObjectFactory.Configure(x =>
                                        {
                                            x.For<IServerView>()
                                                .Use<ServerView>();
                                            x.For<IServerPresentationModel>()
                                                .Use<ServerPresentationModel>();

                                            x.For<IServerDetailsView>()
                                                .Use<ServerDetailsView>();
                                            x.For<IServerDetailsPresentationModel>()
                                                .Use<ServerDetailsPresentationModel>();
                                        });
        }
    }
}
