using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DelegateLearning3
{
    class Program
    {
        static void Main(string[] args)
        {
            Publisher pub = new Publisher();
            Subscriber1 sub1 = new Subscriber1();
            Subscriber2 sub2 = new Subscriber2();
            Subscriber3 sub3 = new Subscriber3();

            pub.MyEvent += new EventHandler(sub1.OnEvent);
            pub.MyEvent += new EventHandler(sub2.OnEvent);
            pub.MyEvent += new EventHandler(sub3.OnEvent);
            pub.DoSomething();
            Console.WriteLine("Control back to client!");
            Console.WriteLine("Press any thing to exit... ");
            Console.ReadLine();
        }

        //public static object[] FireEvent(Delegate del,params object[] args)
        //{
        //    List<object> objList = new List<object>();
        //    if(del!=null)
        //    {
        //        Delegate[] delArray = del.GetInvocationList();
        //        foreach(Delegate method in delArray)
        //        {
        //            try
        //            {
        //                object obj = method.DynamicInvoke(args);
        //                if (obj != null)
        //                    objList.Add(obj);
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine("Exception:{0}", e.Message);
        //            }
        //        }
        //    }
        //    return objList.ToArray();
        //}
    }

    public class Publisher
    {
        public event EventHandler MyEvent;
        public void DoSomething()
        {
            Console.WriteLine("Dosomething invoked!");
            //Program.FireEvent(MyEvent,this,EventArgs.Empty);
            if(MyEvent!=null)
            {
                Delegate[] delArray = MyEvent.GetInvocationList();
                foreach(Delegate del in delArray)
                {
                    EventHandler method = (EventHandler)del;
                    method.BeginInvoke(null,EventArgs.Empty,null,null);
                }
            }
        }
    }

    public class Subscriber1
    {
        public void OnEvent(object sender,EventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(3));
            Console.WriteLine("Waited for 3 seconds,Subscriber1 Invoked!");
        }
    }

    public class Subscriber2
    {
        public void OnEvent(object sender,EventArgs e)
        {
            throw new Exception("Subscriber2 failed!");
            //Console.WriteLine("Subscriber2 immediately invoked!");
        }
    }

    public class Subscriber3
    {
        public void OnEvent(object sender,EventArgs e)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            Console.WriteLine("Waited for 2 seconds,Subscriber3 Invoked!");
        }
    }
}
