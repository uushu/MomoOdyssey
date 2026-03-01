using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PLAYER_TWO.Platformer_Project.Scripts.Player
{
    public class PlayerInputManager: MonoBehaviour
    {
        //主要处理：输入管理、配置数据（速度、方向、加速度、减速度、摩擦力等等）、状态改变
        //输入动作资源（输入配置表）
        public InputActionAsset actions;
        
        
        // 输入动作缓存
        protected InputAction m_movement;
        
        //用于锁定移动方向的时间戳（小于时间戳则不移动）
        protected float m_lockDirection;
        
        //主摄像机的引用，获取相对移动方向
        protected Camera m_camera;
        
        /// <summary>
        /// 根据输入配置表初始化输入动作资源
        /// </summary>
        protected virtual void CacheActios()
        {
            m_movement = actions["Movement"];
        }
        //调用初始化
        protected virtual void Awake() => CacheActios();

        protected void Start()
        {
            m_camera=Camera.main;
            actions.Enable();
        }

        protected virtual void OnEnable() => actions?.Enable();//打开
        protected virtual void OnDisable() => actions?.Disable();//关闭

        /// <summary>
        /// 获取移动方向输入（带十字形死区判断->有效输入）
        /// 如果在锁定时间内，返回Vector3.zero
        /// </summary>
        public virtual Vector3 GetMovementDirection()
        {
            if (Time.time < m_lockDirection)
            {
                return Vector3.zero;
            }
            var value=m_movement.ReadValue<Vector2>();
            return GetAxisWithCrossDeadZone(value);
        }

        /// <summary>
        /// 根据十字形死区修正输入值（InputSystem默认是圆形死区）
        /// </summary>
        private Vector3 GetAxisWithCrossDeadZone(Vector2 axis)
        {
            var deadzone = InputSystem.settings.defaultDeadzoneMin;
            axis.x = Mathf.Abs(axis.x) > deadzone ? RemapToDeadZone(axis.x, deadzone) : 0;
            axis.y = Mathf.Abs(axis.y)>deadzone?RemapToDeadZone(axis.y,deadzone):0;
            return new Vector3(axis.x, 0, axis.y);
        }
        /// <summary>
        /// 将输入值按给定死去重新映射到 0-1（deadzone）
        /// </summary>
        protected float RemapToDeadZone(float value, float deadzone)
        {
            return (value - (value > 0 ? deadzone : -deadzone)) / (1 - deadzone);
        }


        /// <summary>
        /// 当玩家转向时，实现摄像机的旋转，返回最终的世界空间移动方向
        /// </summary>
        /// <returns></returns>
        public  virtual Vector3 GetMovementCameraDirction()
        {
            //获取移动方向（通常是玩家输入的水平或垂直方向，比如WASD）
            var dirction = GetMovementDirection();
            
            //如果有输入
            if (dirction.sqrMagnitude > 0)
            {
                //构建一个旋转，根据摄像机Y轴角度，水平朝向
                // Quaternion.AngleAxis(angle, axis) 表示绕某一个轴转一个角度
                var rotation=Quaternion.AngleAxis(m_camera.transform.eulerAngles.y, Vector3.up);
                
                //将移动方向旋转到摄像机方向
                dirction = rotation * dirction;
                
                //归一化，保持方向向量长度为1
                dirction = dirction.normalized;
                
            }
            
            //返回最终世界空间移动方向
            return dirction;
        }
    }
}