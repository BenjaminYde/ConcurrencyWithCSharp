using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConcurrencyWithCSharp
{
    public class _15AsyncStateObjects
    {
        private static void Main()
        {
            //FailedExample();
            //WorkingExample();
            WorkingExampleVariant();
        }
        
        private class CustomData
        {
            public long CreationTime;
            public int Name;
            public int ThreadNum;
        }

        private static void FailedExample()
        {
            //  create task array
            int taskCount = 5;
            Task[] taskArray = new Task[taskCount];
            
            // start new tasks
            for (int i = 0; i < taskCount; i++) 
            {
                // create action
                Action action = () =>
                {
                    var data = new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks };
                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}.", data.Name, data.CreationTime, data.ThreadNum);
                };
                
                // start task
                taskArray[i] = Task.Factory.StartNew(action);
            }
            Task.WaitAll(taskArray);
        }
        
        private static void WorkingExample()
        {
            //  create task array
            int taskCount = 5;
            Task[] taskArray = new Task[taskCount];
            
            // start new tasks
            for (int i = 0; i < taskCount; i++) 
            {
                // create action
                Action<object> action = (object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;
                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                    Console.WriteLine("Task #{0} created at {1} on thread #{2}.", data.Name, data.CreationTime, data.ThreadNum);
                };
                
                // start task
                var stateObject = new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks };
                taskArray[i] = Task.Factory.StartNew(action, stateObject);
            }
            Task.WaitAll(taskArray);
        }
        
        private static void WorkingExampleVariant()
        {
            //  create task array
            int taskCount = 5;
            Task[] taskArray = new Task[taskCount];
            
            // start new tasks
            for (int i = 0; i < taskCount; i++) 
            {
                // create action
                Action<object> action = (object obj) =>
                {
                    CustomData data = obj as CustomData;
                    if (data == null)
                        return;
                    data.ThreadNum = Thread.CurrentThread.ManagedThreadId;
                };
                
                // start task
                var stateObject = new CustomData() { Name = i, CreationTime = DateTime.Now.Ticks };
                taskArray[i] = Task.Factory.StartNew(action, stateObject);
            }
            
            // wait
            Task.WaitAll(taskArray);

            // display data
            foreach (var task in taskArray)
            {
                CustomData data = task.AsyncState as CustomData;
                if (data == null)
                    return;
                Console.WriteLine("Task #{0} created at {1} on thread #{2}.", data.Name, data.CreationTime, data.ThreadNum);
            }
        }
    }
}