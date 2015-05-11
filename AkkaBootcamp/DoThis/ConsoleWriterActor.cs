using System;
using Akka.Actor;

namespace WinTail
{
    /// <summary>
    /// Actor responsible for serializing message writes to the console.
    /// (write one message at a time, champ :)
    /// </summary>
    class ConsoleWriterActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is Messages.InputError)
            {
                var reason = (message as Messages.InputError).Reason;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(reason);
            }
            else if (message is Messages.InputSuccess)
            {
                var reason = (message as Messages.InputSuccess).Reason;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(reason);
            }
            else
            {
                Console.WriteLine(message);
            }
            
            Console.ResetColor();
        }
    }
}
