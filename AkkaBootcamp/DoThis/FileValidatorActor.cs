using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WinTail
{
    public class FileValidatorActor:UntypedActor
    {
        private readonly IActorRef _consoleWriterActor;
        private readonly IActorRef _tailCoordinatorActor;

        public FileValidatorActor(IActorRef consoleWriterActor, IActorRef tailCoordinatorActor)
        {
            _consoleWriterActor = consoleWriterActor;
            _tailCoordinatorActor = tailCoordinatorActor;
        }

        protected override void OnReceive(object msg)
        {
            var message = msg as string;
            if (string.IsNullOrEmpty(message))
            {
                _consoleWriterActor.Tell(new Messages.NullInputError("Null input"));

                Sender.Tell(new Messages.ContinueProccessing());
            }
            else
            {
                var isValid = IsFileUri(message);
                if (isValid)
                {
                    _consoleWriterActor.Tell(new Messages.InputSuccess("Valid input, well done."));

                    _tailCoordinatorActor.Tell(new TailCoordinatorActor.StartTail(message, _consoleWriterActor));
                }
                else
                {
                    _consoleWriterActor.Tell(new Messages.ValidationError("File doesn't exist"));

                    Sender.Tell(new Messages.ContinueProccessing());
                }
            }
            Sender.Tell(new Messages.ContinueProccessing());
        }

        private static bool IsFileUri(string path)
        {
            return File.Exists(path);
        }
    }
}
