using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Akka.Actor;
using Akka.Routing;

namespace ChartApp.Actors
{
    public class PerformanceCounterCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _chartingActor;
        private readonly Dictionary<CounterType, IActorRef> _counterActors;

        #region Message types

        public class Watch
        {
            public CounterType Counter { get; private set; }

            public Watch(CounterType counter)
            {
                Counter = counter;
            }
        }
        public class Unwatch
        {
            public CounterType Counter { get; private set; }

            public Unwatch(CounterType counter)
            {
                Counter = counter;
            }
        }
        #endregion

        private static Dictionary<CounterType, Func<PerformanceCounter>> CounterGenerators = new Dictionary
            <CounterType, Func<PerformanceCounter>>()
        {
            {CounterType.Cpu, () => new PerformanceCounter("Processor", "% Processor Time", "_Total", true)},
            {CounterType.Memory, () => new PerformanceCounter("Memory", "% Committed Bytes In Use", true)},
            {CounterType.Disk, () => new PerformanceCounter("LogicalDisk", "% Disk Time", "_Total", true)}
        };

        private static Dictionary<CounterType, Func<Series>> CounterSeries = new Dictionary<CounterType, Func<Series>>()
        {
            {
                CounterType.Cpu,
                () =>
                    new Series(CounterType.Cpu.ToString())
                    {
                        ChartType = SeriesChartType.SplineArea,
                        Color = Color.DarkGreen
                    }
            },
            {
                CounterType.Memory,
                () =>
                    new Series(CounterType.Memory.ToString())
                    {
                        ChartType = SeriesChartType.FastLine,
                        Color = Color.BlueViolet
                    }
            },
            {
                CounterType.Disk,
                () =>
                    new Series(CounterType.Disk.ToString())
                    {
                        ChartType = SeriesChartType.SplineArea,
                        Color = Color.DarkRed
                    }
            }
        };

        public PerformanceCounterCoordinatorActor(IActorRef chartingActor):this(chartingActor, new Dictionary<CounterType, IActorRef>())
        {
            
        }

        public PerformanceCounterCoordinatorActor(IActorRef chartingActor, Dictionary<CounterType, IActorRef> counterActors )
        {
            _chartingActor = chartingActor;
            _counterActors = counterActors;

            Receive<Watch>(watch =>
            {
                var counterType = watch.Counter;
                if (!_counterActors.ContainsKey(counterType))
                {
                    var counterActor =
                        Context.ActorOf(
                            Props.Create(
                                () =>
                                    new PerformanceCounterActor(counterType.ToString(),
                                        CounterGenerators[counterType])));
                    _counterActors[counterType] = counterActor;
                }

                _chartingActor.Tell(new ChartingActor.AddSeries(CounterSeries[counterType]()));
                _counterActors[counterType].Tell(new SubscribeCounter(counterType, _chartingActor));
            });

            Receive<Unwatch>(unwatch =>
            {
                if (!_counterActors.ContainsKey(unwatch.Counter)) return;

                _counterActors[unwatch.Counter].Tell(new UnsubscribeCounter(unwatch.Counter, _chartingActor));
                _chartingActor.Tell(new ChartingActor.RemoveSeries(unwatch.Counter.ToString()) );
            });
        }
    }
}
