using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using LEAP.Apps.Workbench.Components;
using LEAP.Apps.Workbench.Core.Components;
using LEAP.Apps.Workbench.Core.Events;
using LEAP.Apps.Workbench.Core.Services;
using LEAP.Apps.Workbench.Core.ViewModels;
using LEAP.Apps.Workbench.Views;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace LEAP.Apps.Workbench.ViewModels
{
    public class ApplicationWindowViewModel : BaseViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly ICommandParameterService _commandParameterService;
        private readonly IWorkspaceManagementService _savedFileLoadService;
        private readonly ILogger _logger;

        public ApplicationWindowViewModel()
        {
            Title = "Workbench";
        }

        public ApplicationWindowViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ILoggingService loggingService, ICommandParameterService commandParameterService, IWorkspaceManagementService savedFileLoadService)
        {
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _commandParameterService = commandParameterService;
            _savedFileLoadService = savedFileLoadService;
            _logger = loggingService.CreateLogger();

            Title = "Workbench";

            var items = new List<IDomainExplorerItem>();

            items.Add(new DomainExplorerItem { Name = "Item 1" });
            items.Add(new DomainExplorerItem { Name = "Item 2" });
            items.Add(new DomainExplorerItem { Name = "Item 3", ChildItems=new List<IDomainExplorerItem>
                {
                    new DomainExplorerItem { Name="SubItem1"},
                    new DomainExplorerItem { Name="SubItem2"},
                    new DomainExplorerItem { Name="SubItem3", ChildItems=new List<IDomainExplorerItem>
                {
                    new DomainExplorerItem { Name="SubSubItem1"},
                    new DomainExplorerItem { Name="SubSubItem2"},
                    new DomainExplorerItem { Name="SubSubItem3"}
                }}
                }
            });
            items.Add(new DomainExplorerItem { Name = "Item 4" });

            ExplorerItems = items;

            OnLoadedCommand = new DelegateCommand(OnLoaded);
            DomainExplorerSelectionChangedCommand = new DelegateCommand<IDomainExplorerItem>(x =>
            {
                Debug.WriteLine($"Performing Action on selection. {x.Name}");
                x?.SelectionAction?.Invoke();
            });

            InitialiseNavigationEvents();
        }

        private void InitialiseNavigationEvents()
        {
            _eventAggregator.GetEvent<ShellNavigationRequestEvent>().Subscribe(x =>
            {
                _regionManager.RequestNavigate("WorkspaceRegion", "ViewImagePage");

            }, ThreadOption.UIThread, true);
        }

        public string Title
        {
            get { return GetValue(() => Title); }
            set { SetValue(() => Title, value); }
        }

        public string Status
        {
            get { return GetValue(() => Status); }
            set { SetValue(() => Status, value); }
        }

        public IList<IDomainExplorerItem> ExplorerItems
        {
            get { return GetValue(() => ExplorerItems); }
            set { SetValue(() => ExplorerItems, value); }
        }

        public ICommand OnLoadedCommand
        {
            get { return GetValue(() => OnLoadedCommand); }
            set { SetValue(() => OnLoadedCommand, value); }
        }

        public ICommand DomainExplorerSelectionChangedCommand
        {
            get { return GetValue(() => DomainExplorerSelectionChangedCommand); }
            set { SetValue(() => DomainExplorerSelectionChangedCommand, value); }
        }

        private void OnLoaded()
        {
            _logger.Info("Application Ready");

            var filePath = _commandParameterService.GetValue("launchfilepath");
            var fileExtension = _commandParameterService.GetValue("launchfileextension");

            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(fileExtension))
            {
                // Open File.
                _logger.Debug($"Opening File [{filePath}] with extension [{fileExtension}].");

                try
                {
                    var loaders = _savedFileLoadService.GetLoaders(fileExtension.Replace(".", ""));

                    var loader = loaders.FirstOrDefault();
                    var data = loader.LoadFile(filePath);

                    loader.GetActioners().FirstOrDefault().ActionFile(data);
                }
                catch (Exception e)
                {
                    // File load failed. Do something?
                    _logger.Error($"Failed Opening File [{filePath}].");
                    _logger.Error(e);

                    _regionManager.RequestNavigate("WorkspaceRegion", nameof(EmptyWorkspacePage));
                }
            }
            else
            {
                // Show Empty Workspace
                _logger.Debug("No file etc specified.");

                _regionManager.RequestNavigate("WorkspaceRegion", nameof(EmptyWorkspacePage));
            }
        }
    }
}
