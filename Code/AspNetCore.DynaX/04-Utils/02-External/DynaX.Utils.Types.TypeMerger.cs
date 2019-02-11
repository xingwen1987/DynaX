using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace AspNetCore.DynaX
{
    /// <summary>
    /// DynaX 工具集合
    /// </summary>
    public static partial class DynaX
    {
        /// <summary>
        /// DynaX Utils 扩展集合
        /// </summary>
        public static partial class Utils
        {
            /// <summary>
            /// DynaX Utils Types 扩展集合
            /// </summary>
            public static partial class Types
            {
                public class TypeMergerPolicy
                {
                    internal IList<Tuple<string, string>> IgnoredProperties { get; } = new List<Tuple<string, string>>();

                    internal IList<Tuple<string, string>> UseProperties { get; } = new List<Tuple<string, string>>();

                    public TypeMergerPolicy Ignore(Expression<Func<object>> ignoreProperty)
                    {
                        IgnoredProperties.Add(GetObjectTypeAndProperty(ignoreProperty));
                        return this;
                    }

                    public TypeMergerPolicy Use(Expression<Func<object>> useProperty)
                    {
                        UseProperties.Add(GetObjectTypeAndProperty(useProperty));
                        return this;
                    }

                    public object Merge(object values1, object values2)
                    {
                        return Merger.Merge(values1, values2, this);
                    }

                    private static Tuple<string, string> GetObjectTypeAndProperty(Expression<Func<object>> property)
                    {
                        var objType = string.Empty;
                        var propName = string.Empty;

                        try
                        {
                            if (property.Body is MemberExpression)
                            {
                                objType = ((MemberExpression)property.Body).Member.ReflectedType?.UnderlyingSystemType.Name;
                                propName = ((MemberExpression)property.Body).Member.Name;
                            }
                            else if (property.Body is UnaryExpression)
                            {
                                objType = ((MemberExpression)((UnaryExpression)property.Body).Operand).Member.ReflectedType?.UnderlyingSystemType.Name;
                                propName = ((MemberExpression)((UnaryExpression)property.Body).Operand).Member.Name;
                            }
                            else
                            {
                                throw new Exception("Expression type unknown.");
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error in TypeMergePolicy.GetObjectTypeAndProperty.", ex);
                        }

                        return new Tuple<string, string>(objType, propName);
                    }
                }

                public class Merger
                {
                    private static AssemblyBuilder _asmBuilder;
                    private static ModuleBuilder _modBuilder;
                    private static TypeMergerPolicy _typeMergerPolicy;
                    private static readonly IDictionary<string, Type> AnonymousTypes = new Dictionary<string, Type>();
                    private static readonly object SyncLock = new object();

                    public static object Merge(object values1, object values2)
                    {
                        lock (SyncLock)
                        {
                            var name = $"{values1.GetType()}_{values2.GetType()}";
                            if (_typeMergerPolicy != null)
                            {
                                name += "_" + string.Join(",", _typeMergerPolicy.IgnoredProperties.Select(x => $"{x.Item1}_{x.Item2}"));
                            }
                            var result = CreateInstance(name, values1, values2);
                            if (result != null)
                            {
                                _typeMergerPolicy = null;
                                return result;
                            }
                            var pdc = GetProperties(values1, values2);
                            InitializeAssembly();
                            var newType = CreateType(name, pdc);
                            AnonymousTypes.Add(name, newType);
                            result = CreateInstance(name, values1, values2);
                            _typeMergerPolicy = null;
                            return result;
                        }
                    }

                    internal static object Merge(object values1, object values2, TypeMergerPolicy policy)
                    {
                        _typeMergerPolicy = policy;
                        return Merge(values1, values2);
                    }

                    public static TypeMergerPolicy Ignore(Expression<Func<object>> ignoreProperty)
                    {
                        return new TypeMergerPolicy().Ignore(ignoreProperty);
                    }

                    public static TypeMergerPolicy Use(Expression<Func<object>> useProperty)
                    {
                        return new TypeMergerPolicy().Use(useProperty);
                    }

                    private static object CreateInstance(string name, object values1, object values2)
                    {
                        object newValues = null;
                        if (!AnonymousTypes.ContainsKey(name)) return null;
                        var allValues = MergeValues(values1, values2);
                        var type = AnonymousTypes[name];
                        if (type != null)
                        {
                            newValues = Activator.CreateInstance(type, allValues);
                        }
                        else
                        {
                            lock (SyncLock) AnonymousTypes.Remove(name);
                        }
                        return newValues;
                    }

                    private static PropertyDescriptor[] GetProperties(object values1, object values2)
                    {
                        var properties = new List<PropertyDescriptor>();
                        var pdc1 = TypeDescriptor.GetProperties(values1);
                        var pdc2 = TypeDescriptor.GetProperties(values2);
                        for (var i = 0; i < pdc1.Count; i++)
                        {
                            if (_typeMergerPolicy == null
                                || !_typeMergerPolicy.IgnoredProperties.Contains(new Tuple<string, string>(values1.GetType().Name, pdc1[i].Name))
                                & !_typeMergerPolicy.UseProperties.Contains(new Tuple<string, string>(values2.GetType().Name, pdc1[i].Name)))
                                properties.Add(pdc1[i]);
                        }
                        for (var i = 0; i < pdc2.Count; i++)
                        {
                            if (_typeMergerPolicy == null
                                || !_typeMergerPolicy.IgnoredProperties.Contains(new Tuple<string, string>(values2.GetType().Name, pdc2[i].Name))
                                & !_typeMergerPolicy.UseProperties.Contains(new Tuple<string, string>(values1.GetType().Name, pdc2[i].Name)))
                                properties.Add(pdc2[i]);
                        }
                        return properties.ToArray();
                    }

                    private static Type[] GetTypes(PropertyDescriptor[] pdc)
                    {
                        return pdc.Select(type => type.PropertyType).ToArray();
                    }

                    private static object[] MergeValues(object values1, object values2)
                    {
                        var pdc1 = TypeDescriptor.GetProperties(values1);
                        var pdc2 = TypeDescriptor.GetProperties(values2);

                        var values = new List<object>();
                        for (var i = 0; i < pdc1.Count; i++)
                        {
                            if (_typeMergerPolicy == null
                                || !_typeMergerPolicy.IgnoredProperties.Contains(new Tuple<string, string>(values1.GetType().Name, pdc1[i].Name))
                                & !_typeMergerPolicy.UseProperties.Contains(new Tuple<string, string>(values2.GetType().Name, pdc1[i].Name)))
                                values.Add(pdc1[i].GetValue(values1));
                        }

                        for (var i = 0; i < pdc2.Count; i++)
                        {
                            if (_typeMergerPolicy == null
                                || !_typeMergerPolicy.IgnoredProperties.Contains(new Tuple<string, string>(values2.GetType().Name, pdc2[i].Name))
                                & !_typeMergerPolicy.UseProperties.Contains(new Tuple<string, string>(values1.GetType().Name, pdc2[i].Name)))
                                values.Add(pdc2[i].GetValue(values2));
                        }
                        return values.ToArray();
                    }

                    private static void InitializeAssembly()
                    {
                        if (_asmBuilder != null) return;
                        var assembly = new AssemblyName { Name = "AnonymousTypeExentions" };
                        _asmBuilder = AssemblyBuilder.DefineDynamicAssembly(assembly, AssemblyBuilderAccess.Run);
                        _modBuilder = _asmBuilder.DefineDynamicModule(_asmBuilder.GetName().Name);
                    }

                    private static Type CreateType(string name, PropertyDescriptor[] pdc)
                    {
                        var typeBuilder = CreateTypeBuilder(name);
                        var types = GetTypes(pdc);
                        var fields = BuildFields(typeBuilder, pdc);
                        BuildCtor(typeBuilder, fields, types);
                        BuildProperties(typeBuilder, fields);
                        return typeBuilder.CreateTypeInfo().AsType();
                    }

                    private static TypeBuilder CreateTypeBuilder(string typeName)
                    {
                        var typeBuilder = _modBuilder.DefineType(typeName, TypeAttributes.Public, typeof(object));
                        return typeBuilder;
                    }

                    private static void BuildCtor(TypeBuilder typeBuilder, FieldBuilder[] fields, Type[] types)
                    {
                        var ctor = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, types);
                        var ctorGen = ctor.GetILGenerator();
                        for (var i = 0; i < fields.Length; i++)
                        {
                            ctorGen.Emit(OpCodes.Ldarg_0);
                            ctorGen.Emit(OpCodes.Ldarg, i + 1);
                            ctorGen.Emit(OpCodes.Stfld, fields[i]);
                        }
                        ctorGen.Emit(OpCodes.Ret);
                    }

                    private static FieldBuilder[] BuildFields(TypeBuilder typeBuilder, PropertyDescriptor[] pdc)
                    {
                        var fields = new List<FieldBuilder>();
                        foreach (var pd in pdc)
                        {
                            var field = typeBuilder.DefineField($"_{pd.Name}", pd.PropertyType, FieldAttributes.Private);
                            if (fields.Contains(field) == false) fields.Add(field);
                        }
                        return fields.ToArray();
                    }

                    private static void BuildProperties(TypeBuilder typeBuilder, FieldBuilder[] fields)
                    {
                        foreach (var field in fields)
                        {
                            var propertyName = field.Name.Substring(1);
                            var property = typeBuilder.DefineProperty(propertyName, PropertyAttributes.None, field.FieldType, null);
                            var getMethod = typeBuilder.DefineMethod($"Get_{propertyName}", MethodAttributes.Public, field.FieldType, Type.EmptyTypes);
                            var methGen = getMethod.GetILGenerator();
                            methGen.Emit(OpCodes.Ldarg_0);
                            methGen.Emit(OpCodes.Ldfld, field);
                            methGen.Emit(OpCodes.Ret);
                            property.SetGetMethod(getMethod);
                        }
                    }
                }
            }
        }
    }
}
