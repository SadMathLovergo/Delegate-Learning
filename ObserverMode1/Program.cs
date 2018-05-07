using System;
using System.Collections.Generic;

namespace ObserverMode1
{
    public interface IObservable
    {
        void Register(IObserver obj);
        void Unregister(IObserver obj);
    }

    public interface IObserver
    {
        void Updata();
    }

    public abstract class SubjectBase:IObservable
    {
        private List<IObserver> container = new List<IObserver>();

        public void Register(IObserver obj)
        {
            container.Add(obj);
        }

        public void Unregister(IObserver obj)
        {
            container.Remove(obj);
        }

        protected virtual void Notify()
        {
            foreach(IObserver observer in container)
            {
                observer.Updata();
            }
        }
    }

    public class Hearter:SubjectBase
    {
        private string type;
        private string area;
        private int temprature;

        public string Type
        {
            get { return type; }
        }
        public string Area
        {
            get { return area; }
        }

        public Hearter(string type,string area,int temprature)
        {
            this.type = type;
            this.area = area;
            this.temprature = temprature;
        }

        public Hearter() : this("RealFire 001", "China Xi'an", 0) { }

        protected virtual void OnBoiled()
        {
            base.Notify();
        }

        public void BoilWater()
        {
            for(int i=0;i<=99;i++)
            {
                temprature=i+1;
                if(temprature>97)
                {
                    OnBoiled();
                }
            }
        }
    }

    public class Screen:IObserver
    {
        public void Updata()
        {
            Console.WriteLine("Screen".PadRight(7)+"：水快烧开了！");
        }
    }

    public class Alarm:IObserver
    {
        public void Updata()
        {
            Console.WriteLine("Alarm".PadRight(7)+"：嘟嘟嘟，水温快烧开了！");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Hearter hearter = new Hearter();
            Screen screen = new Screen();
            Alarm alarm = new Alarm();

            hearter.Register(screen);
            hearter.Register(alarm);

            hearter.BoilWater();

            hearter.Unregister(alarm);
            Console.WriteLine();

            hearter.BoilWater();

            Console.ReadLine();
        }
    }
}
