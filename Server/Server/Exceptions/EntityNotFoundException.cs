using System;

namespace ServerArchitecture.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public Type EntityType { get; }

        public object[] EntityKey { get; }

        public EntityNotFoundException(Type entityType, params object[] entityKey)
            : base($"Entity of type {entityType.Name} not found")
        {
            EntityType = entityType;
            EntityKey = entityKey;
        }
    }
}
