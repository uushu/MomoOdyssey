using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Player.state
{
    public class IdlePlayerState:PlayerState
    {
        /// <summary>
        /// 进入待机状态时调用
        /// </summary>
        protected override void OnEnter(Player player)
        {
        }

        /// <summary>
        /// 退出待机状态时调用
        /// </summary>
        
        protected override void OnExit(Player player)
        {
        }
        /// <summary>
        /// 每帧更新待机状态逻辑
        /// </summary>
        protected override void OnStep(Player player)
        {
            //根据输入来获取方向
            var inputDirection = player.inputs.GetMovementDirection();
            //Debug.Log("inputDirection:"+inputDirection);
            //如果移动输入有效或水平速度>0，切换到 walk 状态
            if (inputDirection.sqrMagnitude > 0 || player.lateralVelocity.sqrMagnitude > 0)
            {
                player.states.Change<WalkPlayerState>();
            }
        }
        
        /// <summary>
        /// 碰撞检测，待机时通常不需要额外碰撞处理
        /// </summary>
        public override void OnContact(Player player, Collider other)
        {
        }
        
        
    }
}