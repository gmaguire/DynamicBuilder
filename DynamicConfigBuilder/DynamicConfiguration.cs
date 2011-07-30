using System.Collections.Generic;
using System.Dynamic;

namespace DynamicConfigurationBuilder
{
    public class DynamicConfiguration : DynamicObject
    {
        private readonly Dictionary<string, object> _properties = new Dictionary<string, object>();

        public object this[string index]
        {
            get
            {
                object output;
                return _properties.TryGetValue(index, out output) ? output : null;
            }
            set
            {
                _properties[index] = value;
            }
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (!_properties.TryGetValue(binder.Name, out result))
            {
                result = null;
            }

            return true;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            _properties[binder.Name] = value;
            return true;
        }
    }
}