using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerFacingLeft : IPlayerState
    {
        private float yRotOffset = 270;
        private float yRot;
        private Player player;

        public PlayerFacingLeft(Player player)
        {
            yRot = Rotator.cameraYRot + yRotOffset;
            this.player = player;
            this.player.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        }
        public void Move()
        {
            yRot = Rotator.cameraYRot + yRotOffset;
            player.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
            Vector3 target = player.transform.position + player.transform.forward * PlayerController.speed;
            player.transform.localPosition = Vector3.MoveTowards(player.transform.localPosition, target, PlayerController.speed);
        }
    }
}
