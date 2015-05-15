using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Akka.Actor;
using Akka.Util.Internal;
using ChartApp.Actors;

namespace ChartApp
{
    public partial class Main : Form
    {
        private IActorRef _chartActor;

        private IActorRef _coordinatorActor;
        private readonly Dictionary<CounterType, IActorRef> _toggleActors = new Dictionary<CounterType, IActorRef>(); 

        public Main()
        {
            InitializeComponent();
        }

        #region Initialization


        private void Main_Load(object sender, EventArgs e)
        {
            _chartActor = Program.ChartActors.ActorOf(Props.Create(() => new ChartingActor(sysChart)), "charting");
            _chartActor.Tell(new ChartingActor.InitializeChart(null));

            _coordinatorActor =
                Program.ChartActors.ActorOf(Props.Create(() => new PerformanceCounterCoordinatorActor(_chartActor)),
                    "counters");

            CreateToggleActor(CounterType.Cpu, cpuButton);
            CreateToggleActor(CounterType.Memory, memoryButton);
            CreateToggleActor(CounterType.Disk, diskButton);

            _toggleActors[CounterType.Cpu].Tell(new ButtonToggleActor.Toggle());
        }

        private void CreateToggleActor(CounterType counterType, Button button)
        {
            _toggleActors[counterType] =
                Program.ChartActors.ActorOf(
                    Props.Create(() => new ButtonToggleActor(_coordinatorActor, button, counterType, false))
                        .WithDispatcher("akka.actor.synchronized-dispatcher"));
        }

        #endregion

        private void cpuButton_Click(object sender, EventArgs e)
        {
            _toggleActors[CounterType.Cpu].Tell(new ButtonToggleActor.Toggle());
        }

        private void memoryButton_Click(object sender, EventArgs e)
        {
            _toggleActors[CounterType.Memory].Tell(new ButtonToggleActor.Toggle());
        }

        private void diskButton_Click(object sender, EventArgs e)
        {
            _toggleActors[CounterType.Disk].Tell(new ButtonToggleActor.Toggle());
        }

        private void Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (var toggleActor in _toggleActors.Values )
            {
                toggleActor.Tell(new ButtonToggleActor.Toggle());
            }
            Program.ChartActors.Shutdown();
        }

    }
}
