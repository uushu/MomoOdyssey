using System.Collections.Generic;
using UnityEngine;
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
        //状态进入或退出时广播事件，在外部绑定回调
        public UnityEvent onEnter;
        public UnityEvent onExit;
        
        //记录实体进入改状态后经过的时间，单位秒
        //外部只读，内部或继承类可写
        public float timeSinceEntered { get; protected set; }

        /// <summary>
        /// 触发进入事件，并调用子类实现的OnEnter
        /// </summary>
        /// <param name="entity"></param>
        public void Enter(T entity)
        {
            //进入状态时，重置计时
            timeSinceEntered = 0;
            //触发外部注册的进入事件回调
            onEnter?.Invoke();
            //调用子类自定义的进入逻辑
            OnEnter(entity);
        }
        

        /// <summary>
        /// 退出状态，触发退出事件，并调用子类实现的OnExit
        /// </summary>
        /// <param name="entity"></param>
        public void Exit(T entity)
        {
            //触发外部注册的退出事件回调
            onExit?.Invoke();
            //调用子类自定义的退出逻辑
            OnExit(entity);
        }


        public void Step(T entity)
        {
            //调用子类自定义的持续运行逻辑
            OnStep(entity);
            //累计该状态已持续的时间，单位秒
            timeSinceEntered += Time.deltaTime;
        }

        /// <summary>
        /// 抽象方法，当状态被激活时调用，用于初始化该状态相关逻辑
        /// </summary>
        /// <param name="entity">当前状态所属的实体对象</param>
        protected abstract void OnEnter(T entity);
        /// <summary>
        /// 抽象方法，当状态被退出时调用，用于清理该状态相关逻辑
        /// </summary>
        /// <param name="entity">当前状态所属的实体对象</param>
       
        protected abstract void OnExit(T entity);

        /// <summary>
        /// 抽象方法，当状态持续运行时调用，用于更新该状态持续逻辑
        /// </summary>
        /// <param name="entity">当前状态所属的实体对象</param>
       
        protected abstract void OnStep(T entity);
        /// <summary>
        /// 当尸体与其他碰撞体接触时调用，用于处理碰撞相关逻辑
        /// </summary>
        /// <param name="entity">当前所属实体对象</param>
        /// <param name="other">其他碰到的物体</param>
        public abstract void OnContact(T entity, Collider other);
        
        /// <summary>
        /// 从类型名称字符串创建实体状态对象
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="typeName">要创建的实体状态的完全限定类型名称</param>
        /// <returns>新创建的实体状态对象</returns>
        public static EntityState<T> CreateFromString(string typeName)
        {
            // 使用System.Activator.CreateInstance和System.Type.GetType
            // 根据提供的类型名称字符串创建并返回指定类型的实例
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