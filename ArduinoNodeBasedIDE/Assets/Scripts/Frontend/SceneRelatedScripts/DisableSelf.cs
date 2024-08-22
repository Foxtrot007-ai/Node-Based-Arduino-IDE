using UnityEngine;


//sometimes we don't want to object destroying itself
//it should not be visible on scene
public class DisableSelf : MonoBehaviour
{
    public void Disable()
    {
        gameObject.SetActive(false);
    }
}
