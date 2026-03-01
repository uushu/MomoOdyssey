
using UnityEngine;

namespace PLAYER_TWO.Platformer_Project.Scripts.Player.state
{
    public class WalkPlayerState:PlayerState
    {
        protected override void OnEnter(Player player)
        {
            
        }
        protected override void OnExit(Player player)
        {
            
        }

        protected override void OnStep(Player player)
        {
            //获取玩家输入方向（相机方向）
             var inputDirction = player.inputs.GetMovementCameraDirction();
             if (inputDirction.sqrMagnitude > 0)
             {
                 //输入方向与当前水平速度的点成，用于判断刹车阈值
                 var dot =Vector3.Dot(inputDirction, player.lateralVelocity);
                 if (dot >= player.stats.current.brakeThreshold)
                 {
                     //超过刹车阈值，正常加速与面向方向
                     player.Accelerate(inputDirction);
                     //player.FaceDirctionSmooth(player.lateralVelocity);
                 }
             }
        }

        public override void OnContact(Player player, Collider other)
        {
            
        }
    }
}