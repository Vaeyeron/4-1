using System;
using System.Linq;

namespace ConsoleApp1
{
    class One_Dimensional_Array<T>
    {
        private T[] array;
        private int capacity;
        private int count;

        public delegate bool Condition_Delegate(T item);
        public delegate void Action_Delegate(T item);
        public delegate TResult Projection_Delegate<T, TResult>(T item);

        public One_Dimensional_Array()
        {
            capacity = 10;
            array = new T[capacity];
            count = 0;
        }

        public void Add(T item)
        {
            if (count == capacity)
            {
                Resize();
            }
            array[count] = item;
            count++;
        }

        public void RemoveAt(int index)
        {
            for (int i = index; i < count - 1; i++)
            {
                array[i] = array[i + 1];
            }
            count--;
        }

        private void Resize()
        {
            capacity = capacity * 2 + 1;
            Array.Resize(ref array, capacity);
        }

        public void Sort()
        {
            Array.Sort(array, 0, count);
        }

        public int Count()
        {
            return count;
        }

        public int Count(Condition_Delegate condition)
        {
            int result = 0;
            foreach (T item in array)
            {
                if (condition(item))
                    result++;
            }
            return result;
        }

        public bool Any(Condition_Delegate condition)
        {
            foreach (T item in array)
            {
                if (condition(item))
                    return true;
            }
            return false;
        }

        public bool All(Condition_Delegate condition)
        {
            foreach (T item in array)
            {
                if (!condition(item))
                    return false;
            }
            return true;
        }

        public bool Contains(T item)
        {
            foreach (T element in array)
            {
                if (item.Equals(element))
                    return true;
            }
            return false;
        }

        public T FirstOrDefault(Condition_Delegate condition)
        {
            foreach (T item in array)
            {
                if (condition(item))
                    return item;
            }
            return default(T);
        }

        public void ForEach(Action_Delegate action)
        {
            foreach (T item in array)
            {
                action(item);
            }
        }

        public T[] Where(Condition_Delegate condition)
        {
            T[] result = new T[count];
            int index = 0;
            foreach (T item in array)
            {
                if (condition(item))
                    result[index++] = item;
            }
            Array.Resize(ref result, index);
            return result;
        }

        public TResult[] Select<TResult>(Projection_Delegate<T, TResult> projection)
        {
            TResult[] result = new TResult[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = projection(array[i]);
            }
            return result;
        }
        public T Min()
        {
            return array.Min();
        }

        public T Max()
        {
            return array.Max();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            One_Dimensional_Array<int> intArray = new One_Dimensional_Array<int>();

            intArray.Add(3);
            intArray.Add(1);
            intArray.Add(2);

            intArray.Sort();

            Console.WriteLine("Sorted Array:");
            intArray.ForEach(Console.WriteLine);

            Console.WriteLine("Min Value: " + intArray.Min());
            Console.WriteLine("Max Value: " + intArray.Max());
            Console.ReadKey();
        }
    }
}
