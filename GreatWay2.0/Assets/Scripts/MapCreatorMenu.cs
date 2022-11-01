using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapCreatorMenu : BasicMenu
{
    [SerializeField] InputField _xField;
    [SerializeField] InputField _yField;
    [SerializeField] Text TextLog;
    [SerializeField] MapController map;
    [SerializeField] BasicMenu GameInterface;

    public void CreateMap()
    {
        if (!CheckSize(_xField.text) || !CheckSize(_yField.text))
            ThrowErrorText("Wrong Size!");
        else if (int.Parse(_xField.text) > 50 || int.Parse(_yField.text) > 50)
            ThrowErrorText("toooooo biiiiiig");
        else
        {
            map.GenerateMap(int.Parse(_xField.text), int.Parse(_yField.text));
            OpenNewMenu(GameInterface);
        }
    }

    private bool CheckSize(string s)
    {
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] < '0' || s[i] > '9')
            {
                return false;
            }
        }
        return s.Length > 0;
    }
}
