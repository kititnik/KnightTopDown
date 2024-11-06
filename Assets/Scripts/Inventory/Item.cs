using System;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private int id;
    public int Id
    {
        get { return id; }
        private set { id = value; }
    }
    
    private int _count = 1;
    public int Count
    {
        get { return _count; }
        private set { _count = value; }
    }

    [SerializeField] private string displayName;

    public string DisplayName
    {
        get { return displayName; }
        private set { displayName = value; }
    }

    public bool SetItemCount(int cnt)
    {
        if(cnt < 1) return false;
        _count = cnt;
        return true;
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
