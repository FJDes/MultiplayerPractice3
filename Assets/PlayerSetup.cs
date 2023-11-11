using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSetup : MonoBehaviour
{
   public Movement movement;

   public GameObject camera;

   public string nickname;

   public void IsLocalPlayer() 
   {
    movement.enabled = true;
    camera.SetActive(true);
   }

   [PunRPC]
   public void SetNickname(string _name)
   {
      nickname = _name;
   }
}
