using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ABOGGUS.PlayerObjects;

public interface IEnemy
{
    public void TakeDamage(float damage, PlayerConstants.DamageSource damageSource);

    public void Push(Vector3 distance);
}
