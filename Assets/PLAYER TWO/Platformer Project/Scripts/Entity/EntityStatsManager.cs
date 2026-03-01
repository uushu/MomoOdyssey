using System;
using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Entity
{
    /// <summary>
    /// 泛型抽象类，用于管理指定类型的属性集，必须继承自EntityStats<T>
    /// </summary>
    /// <typeparam name="T">指定类型</typeparam>
    public class EntityStatsManager<T>:MonoBehaviour where T:EntityStats<T>
    {
        //存放索引可用的属性集（不同难度、不同状态属性）
        public T[] stats;
        //当前激活的属性实例
        public T current { get; protected set; }

        public virtual void Change(int to)
        {
            //确保索引合法
            if (to >= 0 && to < stats.Length)
            {
                //如果切换的不是当前属性，则进行切换
                if (current != stats[to])
                {
                    current=stats[to];
                }
            }
        }

        /// <summary>
        /// 初始化current为stats数组中的第一个属性集
        /// </summary>
        protected void Start()
        {
            if (stats.Length > 0)
            {
                current = stats[0];
            }
        }
    }
}