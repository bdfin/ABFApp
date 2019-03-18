using System;

namespace ABF
{
    public interface IServiceCollection
    {
        object AddIdentity<T1, T2>(Func<object, object> p);
    }
}