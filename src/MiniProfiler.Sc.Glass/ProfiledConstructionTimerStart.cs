using System;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Glass.Mapper;
using Glass.Mapper.Caching;
using Glass.Mapper.Pipelines.ObjectConstruction;

namespace StackExchange.Profiling.Data
{
    public class ProfiledConstructionTimerStart : AbstractObjectConstructionTask
    {
        private ICacheKeyGenerator _cacheKeyGenerator;
        private readonly ILog _log;
        private readonly Config.DebugSettings _debugSettings;

        private readonly AbstractObjectConstructionTask _inner;
        private IDbProfiler _profiler;
        public IDbProfiler Profiler => _profiler;

        public ProfiledConstructionTimerStart(
            ICacheKeyGenerator cacheKeyGenerator,
            Config.DebugSettings debugSettings,
            AbstractObjectConstructionTask inner)
        {
            _cacheKeyGenerator = cacheKeyGenerator;
            _debugSettings = debugSettings;
            Name = "ConstructionTimerStart";
            _inner = inner;
        }

        public override void Execute(ObjectConstructionArgs args)
        {
            var miniProfiler = _profiler as MiniProfiler;
            //if (miniProfiler == null || !miniProfiler.IsActive || miniProfiler.Options?.TrackConnectionOpenClose == false)
            if (miniProfiler == null)
            {
                _inner.Execute(args);
                return;
            }
            using (var timing = new CustomTiming(miniProfiler, "ConstructionTimerStart Execute()",
                _debugSettings.SlowModelThreshold))
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                try
                {
                    _inner.Execute(args);
                }
                finally
                {
                    stopwatch.Stop();
                    if (stopwatch.ElapsedMilliseconds > _debugSettings.SlowModelThreshold)
                    {
                        var key = _cacheKeyGenerator.Generate(args) + "stopwatch";
                        var finaltType = args.Result.GetType();
                        timing.CommandString =
                            string.Format("Slow Glass Model - Time: {0} Cachable: {1} Type: {2} Key: {3}",
                                stopwatch.ElapsedMilliseconds, args.Configuration.Cachable, finaltType.FullName, key);
                    }
                }
            }
        }
    }
}
