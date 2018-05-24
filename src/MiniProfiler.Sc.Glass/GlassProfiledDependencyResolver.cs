using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Glass.Mapper;
using Glass.Mapper.Caching;
using Glass.Mapper.Diagnostics;
using Glass.Mapper.IoC;
using Glass.Mapper.Maps;
using Glass.Mapper.Pipelines.ConfigurationResolver;
using Glass.Mapper.Pipelines.DataMapperResolver;
using Glass.Mapper.Pipelines.ObjectConstruction;
using Glass.Mapper.Pipelines.ObjectSaving;

namespace StackExchange.Profiling.Glassmapper
{
    public class GlassProfiledDependencyResolver : IDependencyResolver
    {
        private readonly IDependencyResolver _inner;

        public GlassProfiledDependencyResolver(IDependencyResolver inner) => _inner = inner;

        public Config GetConfig() =>
            _inner.GetConfig();

        public ILog GetLog() => 
            _inner.GetLog();

        public ICacheManager GetCacheManager() =>
            _inner.GetCacheManager();

        public ModelCounter GetModelCounter() =>
            _inner.GetModelCounter();

        public void Finalise() =>
            _inner.Finalise();

        public IConfigFactory<AbstractDataMapperResolverTask> DataMapperResolverFactory =>
            _inner.DataMapperResolverFactory;

        public IConfigFactory<AbstractDataMapper> DataMapperFactory =>
            _inner.DataMapperFactory;

        public IConfigFactory<AbstractConfigurationResolverTask> ConfigurationResolverFactory =>
            _inner.ConfigurationResolverFactory;

        public IConfigFactory<AbstractObjectConstructionTask> ObjectConstructionFactory =>
            _inner.ObjectConstructionFactory;

        public IConfigFactory<AbstractObjectSavingTask> ObjectSavingFactory =>
            _inner.ObjectSavingFactory;

        public IConfigFactory<IGlassMap> ConfigurationMapFactory =>
            _inner.ConfigurationMapFactory;
    }
}
