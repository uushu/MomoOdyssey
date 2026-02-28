using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Entity
{
    //实体抽象类 抽象出实体的某些属性
    public abstract class EntityBase:MonoBehaviour
    {
    
    }
    
    //泛型约束 T都必须是实体类或继承自实体类
    public abstract class Entity<T>:EntityBase where T :Entity<T>
    {
        
    }
}