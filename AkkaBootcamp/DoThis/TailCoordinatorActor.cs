using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WinTail
{
    public class TailCoordinatorActor:UntypedActor
    {
        #region Message types

        public class StartTail
        {
            public string FilePath { get; private set; }
            public IActorRef ReporterActor { get; private set; }

            public StartTail(string filePath, IActorRef reporterActor)
            {
                FilePath = filePath;
                ReporterActor = reporterActor;
            }
        }

        public class StopTail
        {
            public string FilePath { get; private set; }

            public StopTail(string filePath)
            {
                FilePath = filePath;
            }
        }
        #endregion

        protected override void OnReceive(object message)
        {
            if (message is StartTail)
            {
                var startTail = message as StartTail;
                var tailActor =
                    Context.ActorOf((Props.Create(() => new TailActor(startTail.ReporterActor, startTail.FilePath))),
                        "TailActor");

            }
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(10, TimeSpan.FromSeconds(30),
                x =>
                {
                    if(x is ArithmeticException) return Directive.Resume;
                    else if(x is NotSupportedException) return Directive.Stop;

                    return Directive.Restart;
                });
        }
    }
}
