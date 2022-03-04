using System.Threading;
using System;

void PrintThreadType()
{
    string type = Thread.CurrentThread.IsBackground ? "Background Thread" : "Foreground Thread";
    string threadName = Thread.CurrentThread.Name;
    Console.WriteLine($"Thread: {threadName} is a {type}");
}

var thread = new Thread(PrintThreadType);
var thread2= new Thread(PrintThreadType);

thread.Name = "Thread-1";
thread.IsBackground = true;

thread2.Name = "Thread-2";

thread.Start();
thread2.Start();