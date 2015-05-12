using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WinTail
{
    public class ValidationActor : UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;

        public ValidationActor(IActorRef consoleWriterActor)
        {
            _consoleWriterActor = consoleWriterActor;
        }

        protected override void OnReceive(object msg)
        {
            var message = msg as string;
            if (string.IsNullOrEmpty(message))
            {
                _consoleWriterActor.Tell(new Messages.NullInputError("Null input"));
            }
            else
            {
                var isValid = IsValid(message);
                if (isValid)
                {
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Valid input, well done."));
                }
                else
                {
                    _consoleWriterActor.Tell(new Messages.ValidationError("Input is not valid, booo!"));
                }
            }
            Sender.Tell(new Messages.ContinueProccessing());
        }

        private bool IsValid(string message)
        {
            return message.Length % 2 == 0;
        }
    }
}
