using System;
using Akka.Actor;

namespace WinTail
{
    /// <summary>
    /// Actor responsible for reading FROM the console. 
    /// Also responsible for calling <see cref="ActorSystem.Shutdown"/>.
    /// </summary>
    class ConsoleReaderActor : UntypedActor
    {
        public const string StartCommand = "start";
        public const string ExitCommand = "exit";
        private IActorRef _validationActor;

        public ConsoleReaderActor(IActorRef validationActor)
        {
            _validationActor = validationActor;
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }
            
            GetAndValidateInput();
        }

        private void DoPrintInstructions()
        {
            Console.WriteLine("Write something in to console!");
            Console.WriteLine("Some entries will pass validation");
            Console.WriteLine("Type 'exit' to quit");
        }

        private void GetAndValidateInput()
        {
            var message = Console.ReadLine();
            
            if (!string.IsNullOrEmpty(message) && String.Equals(message, ExitCommand, StringComparison.Ordinal))
                Context.System.Shutdown();
            _validationActor.Tell(message);
        }

        private bool IsValid(string message)
        {
            return message.Length%2 == 0;
        }
    }
}