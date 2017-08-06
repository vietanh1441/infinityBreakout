using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : BlockBase
{





    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            //
            base.DoDestroy();
            base.AddMoney();
            //Do death animation in the meantime
        }
    }
}
