// ONI, Copyright (c) Nathan MacAdam, All rights reserved. 
// MIT License (See LICENSE file)

using System.Reflection;

namespace Oni.TestUtilities
{
	public static class Reflection
	{
		private static BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance | BindingFlags.FlattenHierarchy;

        /// <summary>
        /// Sets a field to the given value
        /// </summary>
        /// <remarks>
        /// This method will throw an error if the provided values are incompatible
        /// </remarks>
        /// <param name="obj">The object that implements the field</param>
        /// <param name="fieldName">The name of the field</param>
        /// <param name="value">The value to set the field to</param>
        /// <typeparam name="T">The type of object implementing the field</typeparam>
        public static void SetField<T>(T obj, string fieldName, object value)
		{
			FieldInfo info = typeof(T).GetField(fieldName, flags);

			if (info == null)
			{
				throw new System.Exception($"Object of type {typeof(T).Name} does not have an accessible {fieldName} field");
			}
			else
			{
                if (!info.FieldType.IsAssignableFrom(value.GetType()))
                {
                    throw new System.Exception($"Value of type {value.GetType().Name} is not assignable to the {fieldName} field of type {info.FieldType.Name}");
                }

				info.SetValue(obj, value);
			}
		}

        /// <summary>
        /// Gets the value of a field
        /// </summary>
        /// <remarks>
        /// This method will throw an error if the provided values are incompatible
        /// </remarks>
        /// <param name="obj">The object that implements the field</param>
        /// <param name="fieldName">The name of the field</param>
        /// <typeparam name="TObj">The type of object implementing the field</typeparam>
        /// <typeparam name="TValue">The type of the field being retrieved</typeparam>
        /// <returns>The value of the field</returns>
        public static TValue GetField<TObj, TValue>(TObj obj, string fieldName)
        {
            FieldInfo info = typeof(TObj).GetField(fieldName, flags);

			if (info == null)
			{
				throw new System.Exception($"Object of type {typeof(TObj).Name} does not have an accessible {fieldName} field");
			}
            else
            {   
                if (!info.FieldType.IsAssignableFrom(typeof(TValue)))
                {
                    throw new System.Exception($"Value of type {typeof(TValue).Name} is not assignable to the retrieved {fieldName} field of type {info.FieldType.Name}");
                }

                return (TValue)info.GetValue(obj);
            }
        }

        /// <summary>
        /// Sets a property to the given value
        /// </summary>
        /// <remarks>
        /// This method will throw an error if the provided values are incompatible or if there is no backing field for the property being set
        /// </remarks>
        /// <param name="obj">The object that implements the property</param>
        /// <param name="propertyName">The name of the property</param>
        /// <param name="value">The value to set the property to</param>
        /// <typeparam name="T">The type of object implementing the property</typeparam>
        public static void SetProperty<T>(T obj, string propertyName, object value)
		{
			PropertyInfo info = typeof(T).GetProperty(propertyName, flags);

			if (info == null)
			{
				throw new System.Exception($"Object of type {typeof(T).Name} does not have an accessible {propertyName} property");
			}
			else
			{
                if (!info.PropertyType.IsAssignableFrom(value.GetType()))
                {
                    throw new System.Exception($"Value of type {value.GetType().Name} is not assignable to the {propertyName} property of type {info.PropertyType.Name}");
                }

                if (!info.CanWrite)
                {
                    var field = typeof(T).GetField($"<{propertyName}>k__BackingField", flags);

                    if (field == null)
                    {
                        throw new System.Exception($"There is no backing field for property {propertyName} to be set");
                    }
                    else
                    {
                        field.SetValue(obj, value);
                    }
                }
                else
                {
                    info.SetValue(obj, value);
                }
			}
		}

        /// <summary>
        /// Gets the value of a property
        /// </summary>
        /// <remarks>
        /// This method will throw an error if the provided values are incompatible
        /// </remarks>
        /// <param name="obj">The object that implements the property</param>
        /// <param name="propertyName">The name of the property</param>
        /// <typeparam name="TObj">The type of object implementing the property</typeparam>
        /// <typeparam name="TValue">The type of the property being retrieved</typeparam>
        /// <returns>The value of the property</returns>
        public static TValue GetProperty<TObj, TValue>(TObj obj, string propertyName)
        {
            PropertyInfo info = typeof(TObj).GetProperty(propertyName, flags);

			if (info == null)
			{
				throw new System.Exception($"Object of type {typeof(TObj).Name} does not have an accessible {propertyName} property");
			}
            else
            {   
                if (!info.PropertyType.IsAssignableFrom(typeof(TValue)))
                {
                    throw new System.Exception($"Value of type {typeof(TValue).Name} is not assignable to the retrieved {propertyName} property of type {info.PropertyType.Name}");
                }

                return (TValue)info.GetValue(obj);
            }
        }

        /// <summary>
        /// Invokes a method implemented in an object
        /// </summary>
        /// <remarks>
        /// This method will throw an error if the provided values are incompatible
        /// </remarks>
        /// <param name="obj">The object that implements the field</param>
        /// <param name="methodName">The name of the method</param>
        /// <param name="parameters">The parameters of the method (if any)</param>
        /// <typeparam name="T">The type of object implementing the method</typeparam>
        public static void InvokeMethod<T>(T obj, string methodName, params object[] parameters)
        {
            MethodInfo info = typeof(T).GetMethod(methodName, flags);

			if (info == null)
			{
				throw new System.Exception($"Object of type {typeof(T).Name} does not have an accessible {methodName} method");
			}
			else
			{
				info.Invoke(obj, parameters);
			}
        }

        /// <summary>
        /// Returns whether an object implements a method or not
        /// </summary>
        /// <param name="methodName">The name of the method</param>
        /// <typeparam name="T">The type of object implementing the method</typeparam>
        /// <returns>Whether an object implements a method or not</returns>
        public static bool HasMethod<T>(string methodName)
        {
            MethodInfo info = typeof(T).GetMethod(methodName, flags);

            return info != null;
        }
	}
}