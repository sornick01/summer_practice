using System;
using System.Collections.Generic;
using System.Drawing;

namespace sm_lab3
{
    public class Cache<T>
    {
        private int MaxSize;
        private TimeSpan LifeTime;
        private Dictionary<string, Tuple<T, DateTime>> Memory;

        public Cache (TimeSpan time, int size)
        {
            Memory = new Dictionary<string, Tuple<T, DateTime>>();
            LifeTime = time;
            MaxSize = size;
        }
        
        private void RemoveOldest()
        {
            
            DateTime currentTime = DateTime.Now;
            string theOldest = null;
            TimeSpan maxInterval = new TimeSpan(0);
            TimeSpan interval;
            foreach (var element in Memory)
            {
                interval = currentTime - element.Value.Item2;
                if (interval > maxInterval)
                {
                    maxInterval = interval;
                    theOldest = element.Key;
                }
            }

            Memory.Remove(theOldest);
        }

        private void RemoveExpiredLifetime()
        {
            TimeSpan interval;
            foreach (var element in Memory)
            {
                interval = DateTime.Now - element.Value.Item2;
                if (interval >= LifeTime)
                    Memory.Remove(element.Key);
            }
        }

        public void Save(string key, T data)
        {
            RemoveExpiredLifetime();
            if (Memory.ContainsKey(key))
            {
                throw new ArgumentException();
            }
            else if (Memory.Count == MaxSize)
            {
                RemoveOldest();
                Memory.Add(key, new Tuple<T, DateTime>(data, DateTime.Now));
            }
            else 
                Memory.Add(key, new Tuple<T, DateTime>(data, DateTime.Now));
        }

        public T Get(string key)
        {
            RemoveExpiredLifetime();
            Tuple<T, DateTime> value;
            if (!Memory.TryGetValue(key, out value))
                throw new KeyNotFoundException();
            return value.Item1;
        }
    }
}