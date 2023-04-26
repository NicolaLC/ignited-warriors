using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;

namespace ReflectionSystem
{
    public enum EStatPropertyName
    {
        Health
    }

    [Serializable]
    public class StatProperty<T>
    {
        [SerializeField] private T value;
        private T originalValue;

        public StatProperty(T value)
        {
            this.value = value;
            this.originalValue = value;
        }

        public T GetValue()
        {
            return value;
        }

        public void SetValue(T next)
        {
            value = next;
        }

        public T GetOriginalValue()
        {
            return originalValue;
        }
    }

    public class StatsController : MonoBehaviour
    {
        [SerializeField] private StatProperty<int> Health;

        public UnityEvent<EStatPropertyName> OnStatsUpdate;

        public void SetProperty<T>(EStatPropertyName property, T value)
        {
            FieldInfo prop = GetType().GetField(property.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);

            if (prop == null) return;

            StatProperty<T> nextProp = prop.GetValue(this) as StatProperty<T>;
            
            nextProp.SetValue(value);

            prop.SetValue(this, nextProp);
            
            print($"[{transform.name}] Property {property.ToString()} changed: ${nextProp.GetValue()}");
            
            OnStatsUpdate.Invoke(property);
        }

        public StatProperty<T> GetProperty<T>(EStatPropertyName propertyName)
        {
            // Get the type of this object
            Type type = GetType();

            // Get the property with the given name
            FieldInfo propertyInfo =
                type.GetField(propertyName.ToString(), BindingFlags.NonPublic | BindingFlags.Instance);

            // If the property is not defined, return the default value
            if (propertyInfo == null)
            {
                return default;
            }

            // Return the value of the property
            return (StatProperty<T>)propertyInfo.GetValue(this);
        }
    }
}