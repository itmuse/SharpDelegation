using System;
using System.Linq;
using System.Reflection;

namespace SharpDelegation
{
    public abstract class Delegation
    {
        private static readonly BindingFlags InstanceFlag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic;

        public Delegation() { }

        /// <summary>
        /// Gets the types by objs.
        /// </summary>
        /// <param name="os">The os.</param>
        /// <returns></returns>
        private static Type[] GetTypesByObjs(params object[] os)
        {
            Type[] ts = new Type[os.Length];
            for (int i = 0; i < os.Length; i++)
            {
                ts[i] = os[i].GetType();
            }
            return ts;
        }

        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="t">The t.</param>
        /// <param name="os">The os.</param>
        /// <returns></returns>
        protected static object CreateInstance(Type t, params object[] os)
        {
            Type[] ts = GetTypesByObjs(os);
            ConstructorInfo ci = t.GetConstructor(InstanceFlag, null, ts, null);
            return ci.Invoke(os);
        }

        /// <summary>
        /// Parses the specified delegate object.
        /// </summary>
        /// <param name="delegateObject">The delegate object.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        protected static object Parse(object delegateObject, Type implementationType)
        {
            if (delegateObject == null || implementationType == null)
            {
                throw new ArgumentNullException("delegateObject or implementationType is null.");
            }
            var _type = delegateObject.GetType();
            if (false == _type.IsClass || false == implementationType.IsClass)
            {
                throw new ArgumentOutOfRangeException("delegateObject or implementationType is not class.");
            }
            var properties = _type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            Delegate _delegate = null;

            var instance = CreateInstance(implementationType, new object[] { });

            var nestedTypeName = string.Empty;
            foreach (var nested in _type.GetNestedTypes())
            {
                MethodInfo method = nested.GetMethod("Invoke");
                var piList = from p in properties where p.GetGetMethod().ReturnType == nested select p;

                foreach (var m in implementationType.GetMethods(InstanceFlag))
                {
                    if (nested.Name.StartsWith("_"))
                        nestedTypeName = nested.Name.Substring(1);
                    else
                        nestedTypeName = nested.Name;

                    nestedTypeName = nestedTypeName.Substring(0, 1).ToUpper() + nestedTypeName.Substring(1);

                    if (nestedTypeName == m.Name)
                    {
                        if (false == method.ReturnType.Equals(m.ReturnType))
                            continue;

                        var netestedParas = method.GetParameters();
                        var objParas = m.GetParameters();

                        if (netestedParas.Length != objParas.Length)
                            continue;

                        var allParasSameType = true;
                        for (var i = 0; i < netestedParas.Length; i++)
                        {
                            if (false == netestedParas[i].ParameterType.Equals(objParas[i].ParameterType))
                            {
                                allParasSameType = false;
                                break;
                            }
                        }
                        if (false == allParasSameType)
                            continue;


                        if (m.IsStatic)
                            _delegate = Delegate.CreateDelegate(nested, implementationType, m.Name, false);
                        else
                            _delegate = Delegate.CreateDelegate(nested, instance, m, true);

                        foreach (var pi in piList)
                        {
                            pi.SetValue(delegateObject, _delegate, null);
                        }
                    }
                }
            }
            // TODO: should check all delegate methods has be implemented.
            return delegateObject;
        }
        /// <summary>
        /// New the specified delegate type.
        /// </summary>
        /// <param name="delegateType">Type of the delegate.</param>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static object New(Type delegateType, Type implementationType)
        {
            object instance = CreateInstance(delegateType, new object[] { });
            return Parse(instance, implementationType);
        }
        /// <summary>
        /// New the specified implementation type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="implementationType">Type of the implementation.</param>
        /// <returns></returns>
        public static T New<T>(Type implementationType) where T : new()
        {
            T t = new T();
            return (T)Parse(t, implementationType);
        }
    }
}
