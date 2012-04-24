using System;

namespace StackExchange.StacMan
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class FieldAttribute : Attribute
    {
        public FieldAttribute(string fieldName)
        {
            FieldName = fieldName;
        }

        public readonly string FieldName;
    }
}
