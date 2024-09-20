using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int id;
    public int Id
    {
        get { return id; }
        private set { id = value; }
    }
    

    public virtual void OnPickUp() {
        Destroy(gameObject);
    }

    public override bool Equals(object other)
    {
        if(other == null) return false;
        if(!(other is Item)) return false;

        var item = (Item)other;
        return item.Id == Id;
    }

    public override int GetHashCode()
    {
        return Id;
    }
}
