namespace PLAYER_TWO.Platformer_Project.Scripts.Tools
{
    using System;
    using UnityEngine;

//因为是属性所以要继承
    public class ClassTypeName : PropertyAttribute
    {
        public Type type;

        public ClassTypeName(Type type)
        {
            this.type = type;
        }
    }

}