using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerFacingRight : IPlayerState
    {
        private float yRotOffset = 90;
        private float yRot;
        private GameObject physicalGameObject;

        public PlayerFacingRight(PlayerController playerController)
        {
            yRot = Rotator.cameraYRot + yRotOffset;
            this.physicalGameObject = playerController.GetGameObject();
            this.physicalGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        }
        public void Move()
        {
            yRot = Rotator.cameraYRot + yRotOffset;
            physicalGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
            Vector3 target = physicalGameObject.transform.position + physicalGameObject.transform.forward * PlayerController.speed;
            physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, target, PlayerController.speed);
        }
    }
}