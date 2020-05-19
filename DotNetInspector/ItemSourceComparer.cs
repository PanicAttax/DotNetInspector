using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetInspector
{
    class ItemSourceComparer : IEqualityComparer<IEnumerable>
    {
        public bool Equals(IEnumerable c1, IEnumerable c2)
        {

            if (c1 == null && c2 == null)
            {
                return true;
            }

            var enumerator1 = c1.GetEnumerator();
            var enumerator2 = c2.GetEnumerator();

            while (true)
            {
                if (enumerator1.MoveNext())
                {
                    if (enumerator2.MoveNext())
                    {
                        if (enumerator1.Current != null && enumerator2.Current != null)
                        {
                            if (!(enumerator1.Current.Equals(enumerator2.Current)))
                            {
                                return false;
                            }
                        }
                        else if (enumerator1.Current != null ^ enumerator2.Current != null)
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (enumerator2.MoveNext())
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
        }

        public int GetHashCode(IEnumerable c)
        {
            return base.GetHashCode(); //TODO: write own implemention of GetHashCode method
        }
    }
}
