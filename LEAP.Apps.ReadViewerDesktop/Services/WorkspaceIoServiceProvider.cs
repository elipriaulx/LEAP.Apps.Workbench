using System;
using System.Collections.Generic;
using System.Linq;
using LEAP.Apps.ReadViewerDesktop.Core.Components;
using LEAP.Apps.ReadViewerDesktop.Core.Services;

namespace LEAP.Apps.ReadViewerDesktop.Services
{
    public class WorkspaceIoServiceProvider : IWorkspaceIoService
    {
        private readonly ILogger _logger;
        private readonly Dictionary<string, IList<IFileLoader>> _loaders;

        public WorkspaceIoServiceProvider(ILoggingService loggingService)
        {
            _logger = loggingService.CreateLogger(nameof(WorkspaceIoServiceProvider));

            _loaders = new Dictionary<string, IList<IFileLoader>>();
        }

        public IFileLoader RegisterLoader(string extension, Guid id, string name, Func<string, object> fileLoader)
        {
            extension = extension.Trim().ToLower();

            IList<IFileLoader> loaderBatch = null;

            try
            {
                loaderBatch = _loaders[extension];
            }
            catch
            {

            }

            if (loaderBatch != null && loaderBatch.Any(x => x.Id == id)) return null;

            var loader = new FileLoader
            {
                 Id = id,
                 Name = name,
                 LoadFile = fileLoader
            };

            if (loaderBatch == null)
            {
                loaderBatch = new List<IFileLoader>();
                _loaders[extension] = loaderBatch;
            }

            loaderBatch.Add(loader);

            return loader;
        }

        public void UnregisterLoader(string extension, IFileLoader fileLoader)
        {
            extension = extension.Trim().ToLower();

            IList<IFileLoader> loaderBatch = null;

            try
            {
                loaderBatch = _loaders[extension];
            }
            catch
            {

            }

            loaderBatch?.Remove(fileLoader);
        }

        public IFileLoader GetLoader(string extension, Guid loaderId)
        {
            extension = extension.Trim().ToLower();

            IList<IFileLoader> loaderBatch = null;

            try
            {
                loaderBatch = _loaders[extension];
            }
            catch
            {

            }

            return loaderBatch?.FirstOrDefault(x => x.Id == loaderId);
        }

        public IEnumerable<IFileLoader> GetLoaders(string extension)
        {
            extension = extension.Trim().ToLower();

            IList<IFileLoader> loaderBatch = null;

            try
            {
                loaderBatch = _loaders[extension];
            }
            catch
            {

            }

            return loaderBatch;
        }

        public IReadOnlyDictionary<string, IReadOnlyList<IFileLoader>> GetAllLoaders()
        {
            return _loaders.ToDictionary<KeyValuePair<string, IList<IFileLoader>>, string, IReadOnlyList<IFileLoader>>(i => i.Key, i => i.Value.ToList());
        }
        
        private class FileLoader : IFileLoader
        {
            private readonly IList<IFileActioner> _actioners;

            public Guid Id { get; set; }
            public string Name { get; set; }
            public Func<string, object> LoadFile { get; set; }

            public FileLoader()
            {
                _actioners = new List<IFileActioner>();
            }

            public IFileActioner RegisterActioner(Guid id, string name, Action<object> action)
            {
                // Check if already registered
                var actioner = _actioners.FirstOrDefault(x => x.Id == id);

                if (actioner != null) return null;

                actioner = new FileActioner
                {
                    Id = id,
                    Name = name,
                    Description = string.Empty,
                    ActionFile = action
                };

                _actioners.Add(actioner);

                return actioner;
            }

            public void UnregisterActioner(IFileActioner actioner)
            {
                _actioners.Remove(actioner);
            }

            public IEnumerable<IFileActioner> GetActioners()
            {
                return _actioners;
            }
        }

        private class FileActioner : IFileActioner
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }

            public Action<object> ActionFile { get; set; }
        }
    }
}
