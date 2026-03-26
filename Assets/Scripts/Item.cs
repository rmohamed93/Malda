using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected GameObject owner;
    public virtual void Initialize(GameObject owner)
    {
        this.owner = owner;
    }
    public abstract void Use();
}
