using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace WinTail
{
    public class TailActor:UntypedActor
    {
        private readonly IActorRef _reporterActor;
        private readonly string _filePath;
        private FileObserver _observer;
        private StreamReader _fileStreamReader;

        #region Message types

        public class FileWrite
        {
            public string FileName { get; private set; }

            public FileWrite(string fileName)
            {
                FileName = fileName;
            }
        }

        public class FileError
        {
            public string FileName { get; private set; }
            public string Reason { get; private set; }

            public FileError(string fileName, string reason)
            {
                FileName = fileName;
                Reason = reason;
            }
        }

        public class InitialRead
        {
            public string FileName { get; private set; }
            public string Text { get; private set; }

            public InitialRead(string fileName, string text)
            {
                FileName = fileName;
                Text = text;
            }
        }
        #endregion

        public TailActor(IActorRef reporterActor, string filePath)
        {
            _reporterActor = reporterActor;
            _filePath = filePath;

            _observer = new FileObserver(Self, filePath);
            _observer.Start();

            var fileStream = new FileStream(Path.GetFullPath(filePath), FileMode.Open, FileAccess.Read,
                FileShare.ReadWrite);
            _fileStreamReader = new StreamReader(fileStream, Encoding.UTF8);

            var text = _fileStreamReader.ReadToEnd();
            Self.Tell(new InitialRead(filePath, text));

        }

        protected override void OnReceive(object message)
        {
            if (message is FileWrite)
            {
                var text = _fileStreamReader.ReadToEnd();
                if (!string.IsNullOrEmpty(text))
                {
                    _reporterActor.Tell(text);
                }
            }
            else if (message is FileError)
            {
                var fileError = message as FileError;
                _reporterActor.Tell(string.Format("Tail error: {0}", fileError.Reason));
            }
            else if (message is InitialRead)
            {
                var initialRead = message as InitialRead;
                _reporterActor.Tell(string.Format("Initial Read: {0}", initialRead.Text));
            }
        }
    }
}
