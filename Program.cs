using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDelegation.Test;

namespace SharpDelegation
{
    public class Program
    {
		static StringBuilder sb = new StringBuilder();
		static void PrintMsg(Duck duck)
		{
			sb.Append(duck.Display());
			sb.Append(duck.PerformFly());
			sb.Append(duck.PerformQuack());
		}

        static void Main(string[] args)
        {
			Duck duck = new MallardDuck();
			PrintMsg(duck);
			sb.AppendLine();


			duck = new RedHeadDuck();
			duck.SetFlyBehavior(Delegation.New<FlyBehavior>(typeof(NoVoice)));
			duck.SetQuackBehavior(new QuackBehavior(typeof(Squeak)));
			PrintMsg(duck);
			sb.AppendLine();

			duck = new RedHeadDuck();
			duck.SetQuackBehavior(new QuackBehavior(typeof(NoVoice)));
			PrintMsg(duck);
			sb.AppendLine();

			duck = new RubberDuck();
			duck.SetFlyBehavior(Delegation.New<FlyBehavior>(typeof(FlyNoWay)));//new FlyBehavior(typeof(FlyNoWay))
			duck.SetQuackBehavior(new QuackBehavior(typeof(MuteQuack)));
			PrintMsg(duck);
			sb.Append(duck.Swim());
			sb.AppendLine();

			Console.WriteLine(sb.ToString());
			Console.ReadLine();
        }
    }
	public class NoVoice
	{
		static string Quack()
		{
			return "......";
		}

		public string Fly()
		{
			return "Fly like plane.";
		}
	}
}
