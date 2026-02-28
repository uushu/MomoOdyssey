using System.Collections.Generic;
using PLAYER_TWO.Platformer_Project.Scripts.Entity;
using PLAYER_TWO.Platformer_Project.Scripts.Tools;

using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Player
{
    [RequireComponent(typeof(Player))] //强制要求这个组件所在物体必须挂上Player组件
    public class PlayerStateManager:EntityStateManager<Player>
    {
        /// <summary>
        /// 使用ClassTypeName 特性，在Inspector面板可以下拉选择继承自PlayerState的类
        /// 玩家状态类的字符串数组
        /// </summary>
        [ClassTypeName(typeof(PlayerState))] 
        public string[] states;
        
        /// <summary>
        /// 获取玩家的状态列表
        /// 会将states中的字符串类名数组转换为真正的状态对象列表
        /// </summary>
        /// <returns></returns>
        protected override List<EntityState<Player>> GetStateList()
        {
            return PlayerState.CreateListFromStringArray(states);
        }
    }
    
}