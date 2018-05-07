using System;
using System.Collections.Generic;

namespace DelegateLearning2
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber1 sub1 = new Subscriber1();
            Subscriber2 sub2 = new Subscriber2();
            Subscriber3 sub3 = new Subscriber3();

            pub.Demo += new DemoEventHandler(sub1.OnNumberChanged);
            pub.Demo += new DemoEventHandler(sub2.OnNumberChanged);
            pub.Demo += new DemoEventHandler(sub3.OnNumberChanged);

            List<string> list = pub.DoSomething();
            foreach(string str in list)
            {
                Console.WriteLine(str);
            }
            Console.ReadLine();
        }
    }

    public delegate string DemoEventHandler(int num);

    public class Publisher
    {
        public event DemoEventHandler Demo;
        public List<string> DoSomething()
        {
            List<string> strList = new List<string>();
            if (Demo == null)
                return strList;
            Delegate[] delArray = Demo.GetInvocationList();
            foreach(Delegate del in delArray)
            {
                DemoEventHandler method = (DemoEventHandler)del;
                strList.Add(method(100));
            }
            return strList;
        }
    }

    public class Subscriber1
    {
        public string OnNumberChanged(int num)
        {
            Console.WriteLine("Subscriber1 invoked,number:{0}",num);
            return "[Subscriber1 returned]";
        }
    }

    public class Subscriber2
    {
        public string OnNumberChanged(int num)
        {
            Console.WriteLine("Subscriber2 invoked,number:{0}", num);
            return "[Subscriber2 returned]";
        }
    }

    public class Subscriber3
    {
        public string OnNumberChanged(int num)
        {
            Console.WriteLine("Subscriber3 invoked,number:{0}", num);
            return "[Subscriber3 returned]";
        }
    }
}
