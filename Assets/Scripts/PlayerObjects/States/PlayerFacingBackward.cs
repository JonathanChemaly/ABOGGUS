using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerFacingBackward : IPlayerState
    {
        private float yRotOffset = 180;
        private float yRot;
        GameObject physicalGameObject;

        public PlayerFacingBackward(PlayerController playerController)
        {
            yRot = ThirdPersonCameraController.cameraYRot + yRotOffset;
            this.physicalGameObject = playerController.GetGameObject();
            this.physicalGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
        }
        public void Move()
        {
            yRot = ThirdPersonCameraController.cameraYRot + yRotOffset;
            physicalGameObject.transform.localRotation = Quaternion.Euler(new Vector3(0, yRot, 0));
            Vector3 target = physicalGameObject.transform.position + physicalGameObject.transform.forward * PlayerController.speed;
            physicalGameObject.transform.localPosition = Vector3.MoveTowards(physicalGameObject.transform.localPosition, target, PlayerController.speed);
            //Debug.Log(ThirdPersonCameraController.cameraYRot);
        }
        public void CastMagic(GameObject magicAttackPrefab, bool aoe, PlayerConstants.Magic castType)
        {
            if (aoe)
                Object.Instantiate(magicAttackPrefab, physicalGameObject.transform.position + physicalGameObject.transform.forward * PlayerConstants.WIND_AOE_ATTACK_MAXRANGE + magicAttackPrefab.transform.position, Quaternion.identity);
            else
                Object.Instantiate(magicAttackPrefab, new Vector3(physicalGameObject.transform.position.x, 1.5f, physicalGameObject.transform.position.z), physicalGameObject.transform.rotation);
        }
    }
}