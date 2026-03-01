using  PLAYER_TWO.Platformer_Project.Scripts.Entity;
using PLAYER_TWO.Platformer_Project.Scripts.Player.Stats;
using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Player
{
    public class Player:Entity<Player>
    {
        //玩家输入管理器实例
        public PlayerInputManager inputs { get; protected set; }
        //玩家数据管理器实例
        public PlayerStatsManager stats { get; protected set; }
        
        //初始化输入管理器
        protected virtual void InitializeInputs()=>inputs=GetComponent<PlayerInputManager>();
        protected virtual void InitializeStats()=>stats=GetComponent<PlayerStatsManager>();

        protected override void Awake()
        {
            base.Awake();
            InitializeInputs();
            InitializeStats();
        }

        public virtual void Accelerate(Vector3 direction)
        {
            // 根据是否按下Run键，是否在地面，决定不同的转向阻尼与加速度
            // var turningDrag = isGrounded && inputs.GetRun()
            //     ? stats.current.runingTurningDrag
            //     : stats.current.turningDrag;
            // var acceleration = isGrounded && inputs.GetRun()
            //     ? stats.current.runingTurningDrag
            //     : stats.current.acceleration; 
            // var finalAcceleration=isGrounded?acceleration:stats.current.airAcceleration;//空中加速度
            // var topSpeed=inputs.GetRun()?stats.current.runingTopSpeed:stats.current.topSpeed;//最大速度
            
            
            
            var turningDrag =stats.current.turningDrag;
            var acceleration = stats.current.acceleration; 
            var finalAcceleration=stats.current.airAcceleration;//空中加速度
            var topSpeed=stats.current.topSpeed;//最大速度
            //调用底层Accelerate( 方向、转向阻尼、加速度、最大速度)
            Accelerate(direction, turningDrag, acceleration, topSpeed);
            
            // if (inputs.GetRun())
            // {
            //     lateralVelocity = Vector3.ClampMagnitude(lateralVelocity, topSpeed);
            // }


        }
    }
}