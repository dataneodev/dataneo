using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace dataneo.Helpers
{
    public static class ObjectCreator
    {
        private delegate object ObjectCreatorDelegate();
        private readonly static Dictionary<string, ObjectCreatorDelegate> _objectCreatorsCache =
            new Dictionary<string, ObjectCreatorDelegate>(StringComparer.InvariantCulture);

        /// <summary>
        /// Create instance of a object. 5 times slower than compiled code and 17 faster than Activator class
        /// </summary>
        /// <typeparam name="CType">Returned instance of a object</typeparam>
        /// <param name="typeName">full path namespace to object System.Text.StringBuilder</param>
        /// <returns></returns>
        public static Result<CType> CreateInstance<CType>(string typeName) where CType : class
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentException();

            if (_objectCreatorsCache.TryGetValue(typeName, out ObjectCreatorDelegate creator))
                return GetObjectFromCreator<CType>(creator);

            return CreateObjectCreatorDelegate(typeName)
                    .Tap(ocDelegate => _objectCreatorsCache.Add(ocDelegate.TypeName, ocDelegate.ObjectCreator))
                    .Map(ocDelegate => GetObjectFromCreator<CType>(ocDelegate.ObjectCreator))
                    .Bind(b => b);
        }

        private struct ObjectCreatorResult
        {
            public string TypeName;
            public ObjectCreatorDelegate ObjectCreator;
        }

        private static Result<ObjectCreatorResult> CreateObjectCreatorDelegate(string typeName)
         => Result
            .Success(typeName)
            .OnSuccessTry(tName => Type.GetType(tName), exception => exception?.Message ?? "Error")
            .Ensure(t => t != null, "Class not found")
            .OnSuccessTry(t =>
            {
                var ctor = t.GetConstructor(new Type[0]);
                var methodName = $"{t.Name}Ctor";
                var dm = new DynamicMethod(methodName, t, new Type[0], typeof(object).Module);
                var lgen = dm.GetILGenerator();
                lgen.Emit(OpCodes.Newobj, ctor);
                lgen.Emit(OpCodes.Ret);
                var del = (ObjectCreatorDelegate)dm.CreateDelegate(typeof(ObjectCreatorDelegate));
                return new ObjectCreatorResult
                {
                    ObjectCreator = del,
                    TypeName = t.Name
                };
            }, exception => exception?.Message ?? "Error");

        private static Result<CType> GetObjectFromCreator<CType>(ObjectCreatorDelegate objectCreatorDelegate) where CType : class
        {
            var obj = objectCreatorDelegate?.Invoke() as CType;
            return Result
                .Success(obj)
                .Ensure(obj => obj != null, "Unable to create instance of an object");
        }
    }
}
