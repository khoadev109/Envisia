namespace Envisia.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class PropertyTypeAttribute : Attribute
    {
        public Type[] Types { get; private set; }

        public PropertyTypeAttribute(params Type[] types)
        {
            Types = types;
        }
    }
}
