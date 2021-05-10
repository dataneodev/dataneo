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
        /// Create instance of a object
        /// </summary>
        /// <typeparam name="CType"></typeparam>
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
            .OnSuccessTry(typeName =>
            {
                var t = Type.GetType(typeName);
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
                    TypeName = typeName
                };
            }, exception => exception?.Message ?? "Error");

        private static Result<CType> GetObjectFromCreator<CType>(ObjectCreatorDelegate objectCreatorDelegate) where CType : class
        {
            var obj = objectCreatorDelegate?.Invoke() as CType;
            return obj != null ?
                Result.Success(obj) :
                Result.Failure<CType>("Unable to create instance of object");
        }
    }
}
