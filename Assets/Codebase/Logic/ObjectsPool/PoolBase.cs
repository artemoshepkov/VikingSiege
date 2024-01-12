using System;
using System.Collections.Generic;

namespace Codebase.Logic.ObjectsPool
{
    public class PoolBase<T>
    {
        private readonly Func<T> _preloadFunc;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;

        private readonly Queue<T> _items = new Queue<T>();
        private readonly List<T> _active = new List<T>();

        public PoolBase(Func<T> preloadFunc, Action<T> getAction, Action<T> returnAction, int preloadCount)
        {
            _preloadFunc = preloadFunc;
            _getAction = getAction;
            _returnAction = returnAction;

            if (_preloadFunc == null)
                throw new ArgumentException("Preload function is null");

            for (int i = 0; i < preloadCount; i++) 
                Return(_preloadFunc());
        }

        public T Get()
        {
            T item = _items.Count > 0 ? _items.Dequeue() : _preloadFunc();
            _getAction(item);
            _active.Add(item);

            return item;
        }

        public void Return(T item)
        {
            _returnAction(item);
            _active.Remove(item);
            _items.Enqueue(item);
        }

        public void ReturnAll()
        {
            foreach (T item in _active) 
                Return(item);
        }
    }
}