using System;
namespace ColoradoRoads.iOS.Utils.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NotCachableAttribute : Attribute
    {
        public bool NotCachable { get; private set; }
        
        public NotCachableAttribute()
        {
            NotCachable = true;
        }
    }
}