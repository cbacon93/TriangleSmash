using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBoundaryKiller : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        PlayerController pc = col.GetComponent<PlayerController>();
        EnemyController ec = col.GetComponent<EnemyController>();
        ChestController cc = col.GetComponent<ChestController>();
        ProjectileHandler prc = col.GetComponent<ProjectileHandler>();
        if (pc != null)
            pc.OnHit(999f);
        if (ec != null)
            ec.OnHit();
        if (cc != null)
            cc.OnHit();
        if (cc != null)
            prc.OnHit();
    }
}
