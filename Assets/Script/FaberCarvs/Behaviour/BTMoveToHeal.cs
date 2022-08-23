using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTMoveToHeal : BTMoveToObject
{
    public BTMoveToHeal(MoveTarget target, float range) : base(target, range) { }
    
    protected override bool Condition(Transform _this, GameObject target, float compare)
    {
        if (target.TryGetComponent(out Health health))
        {
            return health.currentLife < health.maxLife;
        }
        return false;
    }
    protected override float Result(Transform _this, GameObject target)
    {
        if (target.TryGetComponent(out Health health))
        {
            _this.GetComponent<Support>().allyToHeal = target;
            return health.currentLife;
        }
        return Mathf.Infinity;
    }
}
