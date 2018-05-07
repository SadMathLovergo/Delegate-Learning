using System;
using System.Collections.Generic;

namespace ObserverMode3
{
    public interface IObservable
    {
        void Register(IObserver obj);
        void Unregister(IObserver obj);
    }

    public interface IObserver
    {
        void Updata(IObservable sender);
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

        protected virtual void Notify(IObservable obj)
        {
            foreach (IObserver observer in container)
            {
                observer.Updata(obj);
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
        public int Temprature
        {
            get { return temprature; }
        }

        public Hearter(string type, string area, int temprature)
        {
            this.type = type;
            this.area = area;
            this.temprature = temprature;
        }

        public Hearter() : this("RealFire 001", "China Xi'an", 0) { }

        protected virtual void OnBoiled()
        {
            base.Notify(this);
        }

        public void BoilWater()
        {
            for (int i = 0; i <= 99; i++)
            {
                temprature = i + 1;
                if (temprature > 97)
                {
                    OnBoiled();
                }
            }
        }
    }

    public class Screen : IObserver
    {
        private bool isDisplayedType = false;
        public void Updata(IObservable sender)
        {
            Hearter hearter = (Hearter)sender;

            if (!isDisplayedType)
            {
                Console.WriteLine("{0}-{1}:", hearter.Area, hearter.Type);
                Console.WriteLine();
                isDisplayedType = true;
            }

            if (hearter.Temprature < 100)
            {
                Console.WriteLine("Alarm".PadRight(8) + "：水快烧开了，当前温度：{0}。", hearter.Temprature);
            }
            else
            {
                Console.WriteLine("Alarm".PadRight(8) + "：水已经烧开了！！");
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
