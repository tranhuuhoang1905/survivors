using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : BulletBase
{
    void Start()
    {
        BulletSoundManager soundManager = GetComponent<BulletSoundManager>();
        BulletMovement movement = GetComponent<BulletMovement>();
        Initialize(soundManager, movement);
    }

}
