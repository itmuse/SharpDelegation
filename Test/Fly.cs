using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpDelegation.Test
{
    public class FlyBehavior
    {
        public delegate string _Fly();
        public _Fly Fly { get; set; }
    }

    public class FlyWithWing
    {

        public string Fly()
        {
            return "Fly with wing, ";
        }
    }

    public class FlyNoWay
    {
        public string Fly()
        {
            return "Fly no way, ";
        }
    }
}
