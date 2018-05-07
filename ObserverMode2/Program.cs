using System;
using System.Collections.Generic;

namespace ObserverMode2
{
    public class BoiledEventArgs
    {
        private int temperature;
        private string type;
        private string area;

        public BoiledEventArgs(int temperature,string type,string area)
        {
            this.temperature = temperature;
            this.type = type;
            this.area = area;
        }

        public int Temperature
        {
            get { return temperature; }
        }
        public string Type
        {
            get { return type; }
        }
        public string Area
        {
            get { return area; }
        }
    }

    public interface IObservable
    {
        void Register(IObserver obj);
        void Unregister(IObserver obj);
    }

    public interface IObserver
    {
        void Updata(BoiledEventArgs e);
    }

    public abstract class SubjectBase : IObservable
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

        protected virtual void Notify(BoiledEventArgs e)
        {
            foreach (IObserver observer in container)
            {
                observer.Updata(e);
            }
        }
    }

    public class Hearter : SubjectBase
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

        public Hearter(string type, string area, int temprature)
        {
            this.type = type;
            this.area = area;
            this.temprature = temprature;
        }

        public Hearter() : this("RealFire 001", "China Xi'an", 0) { }

        protected virtual void OnBoiled(BoiledEventArgs e)
        {
            base.Notify(e);
        }

        public void BoilWater()
        {
            for (int i = 0; i <= 99; i++)
            {
                temprature = i + 1;
                if (temprature > 97)
                {
                    BoiledEventArgs e = new BoiledEventArgs(temprature,type,area);
                    OnBoiled(e);
                }
            }
        }
    }

    public class Screen : IObserver
    {
        private bool isDisplayedType = false;
        public void Updata( BoiledEventArgs e)
        {
            if(!isDisplayedType)
            {
                Console.WriteLine("{0}-{1}:",e.Area,e.Type);
                Console.WriteLine();
                isDisplayedType = true;
            }

            if(e.Temperature<100)
            {
                Console.WriteLine("Alarm".PadRight(8)+"：水快烧开了，当前温度：{0}。",e.Temperature);
            }
            else
            {
                Console.WriteLine("Alarm".PadRight(8)+"：水已经烧开了！！");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Hearter hearter = new Hearter();
            Screen screen = new Screen();
            hearter.Register(screen);
            hearter.BoilWater();

            Console.ReadLine();
        }
    }
}
