using System.Collections.Generic;
using UnityEngine.Events;

namespace PLAYER_TWO.Platformer_Project.Scripts.Entity
{
    
    /// <summary>
    /// 泛型抽象类，表示实体状态中的某一个状态
    /// T必须是继承实体或实体类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityState<T> where T:Entity<T>
    {
        public UnityEvent onEnter;
        public static EntityState<T> CreateFromString(string typeName)
        {
            return (EntityState<T>)System.Activator.CreateInstance(System.Type.GetType(typeName));
        }
        
        
        /// <summary>
        /// 静态方法，根据字符串数组批量创建状态实例列表
        /// </summary>
        /// <param name="array">包含多个状态类名的字符串数组</param>
        /// <returns></returns>
        public static List<EntityState<T>> CreateListFromStringArray(string[] array)
        {
            var list = new List<EntityState<T>>();
            foreach (var typeName in array)
            {
                list.Add(CreateFromString(typeName));
            }

            return list;
        }
    }
}