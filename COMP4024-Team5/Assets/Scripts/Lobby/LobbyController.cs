using UnityEngine;

public class LobbyController : MonoBehaviour
{
    [SerializeField] private GameObject[] components;

    private void Start()
    {
        
        foreach (GameObject component in components)
        {
            if (component != null)
            {
                string componentKey = component.name;
                bool isCollected = CollectionController.Instance.IsItemCollected(componentKey); //CollectionController

                if (isCollected)
                {
                    Debug.Log($" {componentKey} is collected");
                    component.SetActive(true);
                }
                else
                {
                    Debug.Log($" {componentKey} is not collected");
                    component.SetActive(false);
                }
            }
            else
            {
                Debug.LogError(" A component is missing in the LobbyController script");
            }
        }
    }
}