using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TransparencyOnCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (Physics2D.GetIgnoreLayerCollision(gameObject.layer, col.gameObject.layer))
        {
            var spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null) 
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.5f);
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        if (Physics2D.GetIgnoreLayerCollision(gameObject.layer, col.gameObject.layer))
        {
            var spriteRenderer = col.gameObject.GetComponent<SpriteRenderer>();
            if(spriteRenderer != null) 
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 1);
        }
    }
}
