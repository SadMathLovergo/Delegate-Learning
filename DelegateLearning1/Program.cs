using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateLearning1
{
    class Program
    {
        private delegate void processDelegate(string name);
        private static void englishProcess(string name)
        {
            Console.WriteLine("Hello, "+name);
        }
        private static void chineseProcess(string name)
        {
            Console.WriteLine("你好, "+name);
        }
        private static void anotherEnglishProcess(string name)
        {
            Console.WriteLine("Nice to meet you!");
        }
        private static void anotherChineseProcess(string name)
        {
            Console.WriteLine("好久不见!");
        }
        private static void Process(string name,processDelegate processPeople)
        {
            processPeople(name);
        }
        static void Main(string[] args)
        {
            processDelegate delegate1 = new processDelegate(englishProcess);
            delegate1 += anotherEnglishProcess;
            processDelegate delegate2 = new processDelegate(chineseProcess);
            delegate2+= anotherChineseProcess;
            Process("FengCong",delegate1);
            Process("冯聪", delegate2);
        }
    }
}
