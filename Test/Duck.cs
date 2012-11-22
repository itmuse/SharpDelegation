using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDelegation.Test
{
    public class MallardDuck : Duck
    {
        public override string Display()
        {
            return "Mallar Duck, ";
        }
    }

    public class RedHeadDuck : Duck
    {
        public override string Display()
        {
            return "Red Head Duck, ";
        }
    }

    public class RubberDuck : Duck
    {
        public override string Display()
        {
            return "Rubber Duck, ";
        }
    }

    public abstract class Duck
    {
        protected FlyBehavior flyBehavior;
        protected QuackBehavior quackBehavior;

        public Duck()
        {
            //flyBehavior = Delegation.New<FlyBehavior>(typeof(FlyWithWing));
            //quackBehavior = Delegation.New<QuackBehavior>(typeof(Quacks));

            flyBehavior = Delegation.New<FlyBehavior>(typeof(FlyWithWing));
            quackBehavior = new QuackBehavior(typeof(Quacks));
        }

        public string Swim()
        {
            return "Swimming.";
        }

        public string PerformFly()
        {
            return flyBehavior.Fly();
        }

        public string PerformQuack()
        {
            return quackBehavior.Quack();
        }

        public void SetFlyBehavior(FlyBehavior flyBehavior)
        {
            this.flyBehavior = flyBehavior;
        }

        public void SetQuackBehavior(QuackBehavior quackBehavior)
        {
            this.quackBehavior = quackBehavior;
        }

        public abstract string Display();

    }
}
