using System.Collections.Generic;

namespace Backend.API
{
    public interface IManageList<T>
    {
        public List<T> Manages { get; }
        public T Create();
    }
}
