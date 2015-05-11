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
        private IActorRef _consoleWriterActor;

        public ConsoleReaderActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object message)
        {
            if (message.Equals(StartCommand))
            {
                DoPrintInstructions();
            }
            else if (message is Messages.InputError)
            {
                _consoleWriterActor.Tell(message as Messages.InputError);
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
            if(string.IsNullOrEmpty(message))
                Self.Tell(new Messages.NullInputError("No input received"));
            else if (String.Equals(message, ExitCommand, StringComparison.Ordinal))
                Context.System.Shutdown();
            else
            {
                var valid = IsValid(message);
                if (valid)
                {
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Thank you, this is valid entry"));
                    Self.Tell(new Messages.ContinueProccessing());
                }
                else
                {
                    Self.Tell(new Messages.ValidationError("Invalid input"));
                }
            }
        }

        private bool IsValid(string message)
        {
            return message.Length%2 == 0;
        }
    }
}