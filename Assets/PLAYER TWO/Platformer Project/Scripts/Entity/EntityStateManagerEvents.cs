using System;
using UnityEngine.Events;

namespace PLAYER_TWO.Platformer_Project.Scripts.Entity
{
    //可序列化的
    [Serializable]
    public class EntityStateManagerEvents
    {
        //当前状态发生变化时触发的事件
        public UnityEvent onChange;

        //当进入状态时触发的事件
        public UnityEvent<Type> onEnter;
        //当退出状态时触发的事件
        public UnityEvent<Type> onExit;
    }
}