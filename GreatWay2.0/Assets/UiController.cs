using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    [SerializeField] InterfaceContainerScript _interfacePrefab;

    private InterfaceContainerScript PlayerInterface;

    public bool isActive { set { PlayerInterface.gameObject.SetActive(value); } }
    public InterfaceContainerScript playerInteface { get { return PlayerInterface; } }

    private void Awake()
    {
        PlayerInterface = Instantiate(_interfacePrefab, FindObjectOfType<GameInterface>(true).antitiesEnterfaceFolder.transform);
        PlayerInterface.name = transform.name + "_interface";
    }
}
