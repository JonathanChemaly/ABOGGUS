using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ABOGGUS.PlayerObjects
{
    public class PlayerFacingBackRight : IPlayerState
    {
        private float yRotOffset = 135;
        private float yRot;
        GameObject physicalGameObject;

        public PlayerFacingBackRight(PlayerController playerController)
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
        }
        public void CastMagic(GameObject magicAttackPrefab, bool aoe, PlayerConstants.Magic castType)
        {
            if (aoe && castType == PlayerConstants.Magic.Wind)
                Object.Instantiate(magicAttackPrefab, physicalGameObject.transform.position + physicalGameObject.transform.forward * PlayerConstants.WIND_AOE_ATTACK_MAXRANGE + magicAttackPrefab.transform.position, Quaternion.identity);
            else if (aoe && castType == PlayerConstants.Magic.Fire)
                Object.Instantiate(magicAttackPrefab, physicalGameObject.transform.position + physicalGameObject.transform.forward * (magicAttackPrefab.transform.localScale.z / 2 + 1), physicalGameObject.transform.rotation);
            else if (castType == PlayerConstants.Magic.Wind || castType == PlayerConstants.Magic.Fire)
                Object.Instantiate(magicAttackPrefab, new Vector3(physicalGameObject.transform.position.x, 1.5f, physicalGameObject.transform.position.z), physicalGameObject.transform.rotation);
        }
    }
}