using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Entity
{
    /// <summary>
    /// 用于定义实体的数据属性
    /// 继承自ScriptableObject，方便通过Unity配置和管理静态数据资产
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityStats<T>:ScriptableObject where T: ScriptableObject
    {
        
    }
}