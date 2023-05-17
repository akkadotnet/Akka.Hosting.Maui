using System;
using NBench;

namespace Akka.Hosting.Maui.Tests.Performance
{
    class Program
    {
        static int Main(string[] args)
        {
            return NBenchRunner.Run<Program>();
        }
    }
}
