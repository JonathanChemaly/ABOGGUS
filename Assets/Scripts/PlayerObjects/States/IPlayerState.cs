using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public interface IPlayerState
    {
        public void Move();

        public void CastMagic(GameObject magicAttackPrefab, bool aoe, PlayerConstants.Magic castType);
    }
}
