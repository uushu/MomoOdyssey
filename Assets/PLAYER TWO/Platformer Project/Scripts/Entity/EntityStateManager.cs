using System;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Entity
{
    /// <summary>
    /// 实体状态管理器类，用于管理实体的各种状态,带有事件支持
    /// </summary>
    public abstract class EntityStateManager:MonoBehaviour
    {
        //状态管理相关事件集合（进入状态、退出状态、状态切换等）
        //具体定义在EntityStateManagerEvent里
        public EntityStateManagerEvents events;

    }

    /// <summary>
    /// 泛型抽象类，继承自EntityStateManager，管理特定实体类 T的状态机
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class EntityStateManager<T> : EntityStateManager where T : Entity<T>
    {
        //持有所有状态实例的列表，顺序定义状态管理器的状态顺序
        protected List<EntityState<T>> m_list = new List<EntityState<T>>();
        
        //状态字典，键为状态类型，值为对应状态实例，方便快速查找
        protected Dictionary<Type, EntityState<T>> m_states = new Dictionary<Type, EntityState<T>>();
        
        //当前激活的状态实例
        public EntityState<T> current { get; protected set; }
        
        //上一个状态实例
        public EntityState<T> last { get; protected set; }
        
        //当前状态管理器关联的实体对象实例
        public T entity { get; protected set; }

        protected virtual void InitializeEntity() => entity = GetComponent<T>();
        
        /// <summary>
        /// 生命周期Start，负责初始化实体和状态
        /// </summary>
        protected void Start()
        {
            InitializeStates();
            InitializeEntity();
        }
        /// <summary>
        /// 抽象方法，子类必须实现，用于返回所以状态实例的列表
        /// </summary>
        /// <returns></returns>
        protected abstract List<EntityState<T>> GetStateList();

        
        /// <summary>
        /// 初始化状态列表和状态字典
        /// 调用GetStateList()获取状态列表，加入字典以便快速查找
        /// 同时默认将current设置为状态列表的第一个状态（存在的话）
        /// </summary>
        protected virtual void InitializeStates()
        {
            m_list = GetStateList();
            foreach (var state in m_list)
            {
                var type = state.GetType();
                if (!m_states.ContainsKey(type))
                {
                    m_states.Add(type, state);
                }
            }

            if (m_list.Count > 0)
            {
                current = m_list[0];
            }
        }   
        
       
        
        /// <summary>
        /// 每帧调用，用于更新当前状态的逻辑
        /// </summary>
        public virtual void Step()
        {
            //确保当前状态存在且游戏暂未停止
            if (current != null && Time.timeScale > 0)
            {
                current.Step(entity);
            }
        }
        /// <summary>
        /// 根据状态类型泛型参数来切换状态
        /// </summary>
        /// <typeparam name="Tstate"></typeparam>
        public virtual void Change<Tstate>() where Tstate : EntityState<T>
        {
            var type=typeof(Tstate);
            if (m_states.ContainsKey(type))
            {
                Change(m_states[type]);
            }
        }
        /// <summary>
        /// 切换当前状态
        /// 执行状态退出与进入回调，并处罚相关事件
        /// </summary>
        /// <param name="state">目标状态实例</param>
        public virtual void Change(EntityState<T> state)
        {
            if (state != null && Time.timeScale > 0)
            {
                if (current != null)
                {
                    current.Exit(entity);
                    events.onExit.Invoke(current.GetType());
                    last = current;
                }
            }

            current = state;
            current.Enter(entity);
            events.onEnter.Invoke(current.GetType());
            events.onChange?.Invoke();
        }
        
    }
}