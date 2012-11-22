using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDelegation.Test
{
    public class QuackBehavior : Delegation
    {
        public QuackBehavior() { }

        public QuackBehavior(Type implementation)
        {
            Parse(this, implementation);
        }
        public delegate string _Quack();
        public _Quack Quack { get; set; }

    }

    public class Quacks
    {
        public string Quack()
        {
            return "Quack Quack Quack.";
        }
    }

    public class Squeak
    {
        public string Quack()
        {
            return "Squeak Squeak Squeak.";
        }
    }

    public class MuteQuack
    {
        public string Quack()
        {
            return "Mute Quack.";
        }
    }
}
