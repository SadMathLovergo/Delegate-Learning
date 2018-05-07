using System;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace DelegateLearning4
{
    public delegate int AddDelegate(int x,int y);

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Client application started!\n");
            Thread.CurrentThread.Name = "Main Thread";

            Calculator cal = new Calculator();
            AddDelegate del = new AddDelegate(cal.Add);
            string data = "Any data you want to pass.";
            AsyncCallback callback = new AsyncCallback(OnAddComplete);
            del.BeginInvoke(2,5,callback,data);

            for(int i=1;i<=3;i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine("{0}:Add executed {1} second(s).", Thread.CurrentThread.Name, i);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        static int GetReturn(IAsyncResult asyncResult)
        {
            AsyncResult result = (AsyncResult)asyncResult;
            AddDelegate del = (AddDelegate)result.AsyncDelegate;
            int rtn = del.EndInvoke(asyncResult);
            return rtn;
        }

        static void OnAddComplete(IAsyncResult asyncResult)
        {
            AsyncResult result = (AsyncResult)asyncResult;
            AddDelegate del = (AddDelegate)result.AsyncDelegate;
            string data = (string)asyncResult.AsyncState;

            int rtn = del.EndInvoke(asyncResult);
            Console.WriteLine("{0}:Result,{1};Data:{2}\n",Thread.CurrentThread.Name,rtn,data);
        }
    }

    public class Calculator
    {
        public int Add(int x,int y)
        {
            if(Thread.CurrentThread.IsThreadPoolThread)
            {
                Thread.CurrentThread.Name = "Pool Thread";
            }
            Console.WriteLine("Method invoked!");

            for(int i=1;i<=2;i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                Console.WriteLine("{0}:Add executed {1} second(s).",Thread.CurrentThread.Name,i);
            }
            Console.WriteLine("Method complete!");
            return x + y;
        }
    }
}
