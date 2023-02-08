using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerFacingForward : IPlayerState
    {
        private float yRot = 0;
        Player player;

        public PlayerFacingForward(Player player)
        {
            yRot = Rotator.cameraYRot;
            this.player = player;
            this.player.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        }
        public void Move()
        {
            yRot = Rotator.cameraYRot;
            player.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
            Vector3 target = player.transform.position + player.transform.forward * PlayerController.speed;
            player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, target, PlayerController.speed);
        }
    }
}