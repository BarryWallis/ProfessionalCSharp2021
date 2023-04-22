using System.Collections.ObjectModel;

namespace DefaultInterfaceMethods;
public class MyCollection<T> : Collection<T>, IEnumerableEx<T>
{


}
