using System;
using System.Collections.Generic;
using System.Linq;

namespace HSM
{
    internal class CovarianceContainer<Base> where Base : class
    {
        struct Item
        {
            private readonly Type type;
            private readonly Base instance;

            public Item(Type type, Base instance)
            {
                this.type = type;
                this.instance = instance;
            }

            public Type Type => type;

            public Base Instance => instance;
        }

        private List<Item> items = new List<Item>();
        public Base GetInstanceOfExactType(Type type)
        {
            return items
                .Where(item => item.Type == type)
                .Select(item => item.Instance)
                .FirstOrDefault();
        }

        public Base GetInstanceOfExactType<T>() where T: Base => GetInstanceOfExactType(typeof(T));

        public Base GetInstanceWhichVariesOverType(Type type)
        {
            return items
                .Where(item => type.IsAssignableFrom(item.Type))
                .Select(item => item.Instance)
                .FirstOrDefault();
        }
        
        public Base GetInstanceWhichVariesOverType<T>() where T: Base => GetInstanceWhichVariesOverType(typeof(T));

        public void Add(Type type, Base instance)
        {
            items.Add(new Item(type, instance));
        }
        
        public void Add<T>(T instance) where T : Base
        {
            items.Add(new Item(typeof(T), instance));
        }

        public void Clear()
        {
            items.Clear();
        }
    }

    public class TwoCovarianceContainer<Base>
    {
        struct Item
        {
            private readonly Type first;
            private readonly Type second;
            private readonly Base instance;
    
            public Item(Type first, Type second, Base instance)
            {
                this.first = first;
                this.second = second;
                this.instance = instance;
            }
    
            public Type First => first;
    
            public Type Second => second;
    
            public Base Instance => instance;
        }
    
        private readonly List<Item> items = new List<Item>();
    
        public Base GetInstanceOfExactType(Type first, Type second)
        {
            return items
                .Where(item => item.First == first && item.Second == second)
                .Select(item => item.Instance)
                .FirstOrDefault();
        }
    
        public Base GetInstanceOfExactType<U, V>() =>
            GetInstanceOfExactType(typeof(U), typeof(V));
    
        public Base GetInstanceWhichVariesOverType(Type first, Type second)
        {
            return items
                .Where(item => first.IsAssignableFrom(item.First) && second.IsAssignableFrom(item.Second))
                .Select(item => item.Instance)
                .FirstOrDefault();
        }
    
        public Base GetInstanceWhichVariesOverType<U, V>() =>
            GetInstanceWhichVariesOverType(typeof(U), typeof(V));
    
        public void Add(Type first, Type second, Base instance)
        {
            items.Add(new Item(first, second, instance));
        }
    
        public void Add<U, V>(Base instance)
        {
            items.Add(new Item(typeof(U), typeof(V), instance));
        }
    
        public void Clear()
        {
            items.Clear();
        }
    }
}