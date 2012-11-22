SharpDeletation
==========
Interfaces in Go are very powerful, but their power comes from being generic.
Unlike interfaces in Java or C# where an interfaces defines a very specific thing that just lacks an implementation. 

Russ Cox has a really good blog post about [how go interfaces are implemented](http://research.swtch.com/2009/12/go-data-structures-interfaces.html).
Effective Go also has a section on [go interfaces](http://golang.org/doc/effective_go.html#interfaces_and_types).

SharpDeletation is a Go interface feature implementation of C#,it depends on delegate and reflection.


##Getting Started

### How to write interface code.


```c#

    public class FlyBehavior
    {
        public delegate string _Fly();
        public _Fly Fly { get; set; }
    }

```

or 

```c#

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

```

### The implemention.


```c#

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

```

or 

```c#

	public class FlyAndQuack
	{
		public string Fly()
		{
			return "Fly with wing.";
		}

        public string Quack()
        {
            return "Quack Quack Quack.";
        }
	}

```

### Presentation.

```c#

	FlyBehavior fly = Delegation.New<FlyBehavior>(typeof(FlyWithWing));
	fly.Fly();

	fly = Delegation.New<FlyBehavior>(typeof(FlyNoWay));
	fly.Fly();


	QuackBehavior quack = new QuackBehavior(typeof(Squeak));
	quack.Quack();


	quack = new QuackBehavior(typeof(MuteQuack));
	quack.Quack();

```
or

```c#

FlyBehavior fly = Delegation.New<FlyBehavior>(typeof(FlyAndQuack));
fly.Fly();


QuackBehavior quack = new QuackBehavior(typeof(FlyAndQuack));
quack.Quack();

```



##Copyright
Copyright (c) 2012 Hyson Wu. All rights reserved.

##License
Art Mustache is [MIT Lisense](http://www.opensource.org/licenses/mit-license.php)
