using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Kaizen.CMS
{
    public class CM00203Comparer : IEqualityComparer<CM00203>
    {
        private string field;

        private PropertyInfo prop;

        public CM00203Comparer(string field)
        {
            this.field = field;
            prop = typeof(CM00203).GetProperty(field);
        }

        public bool Equals(CM00203 x, CM00203 y)
        {
            var valueX = prop.GetValue(x, null);
            var valueY = prop.GetValue(y, null);
            if (valueX == null)
            {
                return valueY == null;
            }
            return valueX.Equals(valueY);
        }

        public int GetHashCode(CM00203 obj)
        {
            var value = prop.GetValue(obj, null);
            if (value == null)
            {
                return 0;
            }
            return value.GetHashCode();
        }
    }
}
