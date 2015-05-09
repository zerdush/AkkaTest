using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;

namespace AkkaFirst
{

    public class ConstituentMessage
    {
        public decimal Bid { get; private set; }
        public decimal Ask { get; private set; }
        public decimal Weight { get; private set; }

        public ConstituentMessage(decimal bid, decimal ask, decimal weight)
        {
            Weight = weight;
            Ask = ask;
            Bid = bid;
        }
    }

    public class InitializeBasketMessage
    {
        public IList<ConstituentMessage> Basket { get; private set; }

        public InitializeBasketMessage(IList<ConstituentMessage> basket)
        {
            Basket = basket;
        }
    }

    public class BasketPricerActor : ReceiveActor
    {
        public BasketPricerActor()
        {
            //InitializeBasket();
            Receive<InitializeBasketMessage>(
                initializeBasketMessage => Console.WriteLine(initializeBasketMessage.Basket.Count));
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
        }
    }

}
