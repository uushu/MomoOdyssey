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
        //配置数据
        // 当前速度
        public Vector3 velocity { get; set; }

        //当前水平速度（XZ平面速度）
        public Vector3 lateralVelocity
        {
            get { return new Vector3(velocity.x, 0,velocity.z); }
            set { velocity=new Vector3(value.x,velocity.y,value.z); }
        }
        
        //当前垂直速度（Y轴速度）
        public Vector3 verticalVelocity
        {
            get { return new Vector3( 0,velocity.y,0); }
            set { velocity=new Vector3(velocity.x,value.y,velocity.z); }
        }

        public float accelerationMultiplier { get; set; } = 1f;//加速度倍率

        public float gravityMultiplier { get; set; } = 1f;//重力倍率
        public float topSpeedMultiplier { get; set;}= 1f;//最大速度倍率
        public float turningDragMultiplier { get; set; } = 1f;//转向阻力倍率
        public float decelerationMultiplier { get; set; } = 1f;//减速倍率
        
        //状态管理器
        public EntityStateManager<T> states { get; protected set; }
        //初始化状态管理器
        protected virtual void InitializeStateManager() => states = GetComponent<EntityStateManager<T>>();

        protected virtual void Awake()
        {
            InitializeStateManager();
        }
        // 处理状态机的步进逻辑
        protected virtual void HandleStates() => states.Step();

        /// <summary>
        /// 处理角色控制器的移动（改变位置）
        /// </summary>
        protected virtual void HandleController()
        {
            // if (controller.enabled)
            // {
            //     controller.Move(velocity * Time.deltaTime);
            //     return;
            // }

            transform.position += velocity * Time.deltaTime;
        }
        protected virtual void Update()
        {
            HandleStates();
            HandleController();
        }

        public virtual void Accelerate(Vector3 direction, float turningDrag, float acceleration, float topSpeed)
        {
            // 判断方向是否有效（不为零向量）
            if (direction.sqrMagnitude > 0)
            {
                // 计算当前速度在目标方向上的投影速度（标量）
                var speed = Vector3.Dot(direction, lateralVelocity);
                // 计算当前速度在目标方向上的向量部分
                var velocity = direction * speed;
                // 计算当前速度中垂直于目标方向的部分（转向速度）
                var turningVelocity = lateralVelocity - velocity;
                // 计算转向阻力对应的速度变化量（根据转向阻力系数和时间增量）
                var turningDelta = turningDrag * turningDragMultiplier * Time.deltaTime;
                // 计算最大允许速度（考虑速度倍率）
                var targetTopSpeed = topSpeed * topSpeedMultiplier;

                // 如果当前速度未达最大速度，或当前速度与目标方向相反，则加速
                if (lateralVelocity.magnitude < targetTopSpeed || speed < 0)
                {
                    // 增加速度，受加速度倍率和时间影响
                    speed += acceleration * accelerationMultiplier * Time.deltaTime;
                    // 限制速度在[-最大速度, 最大速度]范围内
                    speed = Mathf.Clamp(speed, -targetTopSpeed, targetTopSpeed);
                }

                // 重新计算目标方向速度向量
                velocity = direction * speed;
                // 将转向速度平滑减小到0，实现自然转向过渡
                turningVelocity = Vector3.MoveTowards(turningVelocity, Vector3.zero, turningDelta);
                // 更新横向速度为目标方向速度与转向速度之和
                lateralVelocity = velocity + turningVelocity;
            }
        }
        
    }
}