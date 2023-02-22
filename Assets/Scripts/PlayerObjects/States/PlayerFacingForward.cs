using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerFacingForward : IPlayerState
    {
        private float yRot = 0;
        GameObject physicalGameObject;

        public PlayerFacingForward(PlayerController playerController)
        {
            yRot = Rotator.cameraYRot;
            this.physicalGameObject = playerController.GetGameObject();
            this.physicalGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        }
        public void Move()
        {
            yRot = Rotator.cameraYRot;
            physicalGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
            Vector3 target = physicalGameObject.transform.position + physicalGameObject.transform.forward * PlayerController.speed;
            physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, target, PlayerController.speed);
        }
    }
}