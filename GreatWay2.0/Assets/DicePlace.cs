using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DicePlace : MonoBehaviour
{
    [SerializeField] GameObject _generatePos;
    [SerializeField] BasicDiceScript _dice;
    [SerializeField] int _countOfDices;
    [SerializeField] ResultViuverScript _resultViewer;

    private List<BasicDiceScript> Dices = new List<BasicDiceScript>();
    private List<BasicDiceScript> RestingDices = new List<BasicDiceScript>();

    public ResultViuverScript resultViewer { get { return _resultViewer; } }

    //значения на 1 больше для попадания в пределы случайной генерации
    public Dictionary<DataTypeHolderScript.DiceType, int> maxDiceValue = new Dictionary<DataTypeHolderScript.DiceType, int>()
    {
        {DataTypeHolderScript.DiceType.d4, 5 },
        {DataTypeHolderScript.DiceType.d6,7 },
        {DataTypeHolderScript.DiceType.d8, 9 },
        {DataTypeHolderScript.DiceType.d10, 11 },
        {DataTypeHolderScript.DiceType.d12, 13 },
        {DataTypeHolderScript.DiceType.d20, 21 }
    };

    public int FastDiceRoll(int countOfDices, DataTypeHolderScript.DiceType diceType)
    {
        int maxValue;
        int sumValue = 0;
        if (maxDiceValue.TryGetValue(diceType, out maxValue))
        {
            for (int i = 0; i < countOfDices; i++)
                sumValue += Random.Range(1, maxValue);

            return sumValue;
        }
        else Debug.Log("No maching dice");
        return 0;
    }

    public int FastDiceRoll(int countOfDices, DataTypeHolderScript.DiceType diceType, int throwBonus, int singleBonus)
    {
        int maxValue;
        int sumValue = 0;
        if (maxDiceValue.TryGetValue(diceType, out maxValue))
        {
            for (int i = 0; i < countOfDices; i++)
            {
                int Add = Random.Range(1, maxValue) + throwBonus;
                Add = Add > 0 ? Add : 1;
                sumValue += Add;
            }

            sumValue += singleBonus;
            sumValue = sumValue > 0 ? sumValue : 1;
            return sumValue;
        }
        else Debug.Log("No maching dice");
        return 0;
    }

    private void Awake()
    {
        StartCoroutine(GenerateCourutine(_dice, _countOfDices));
    }

    IEnumerator GenerateCourutine(BasicDiceScript Dice, int Count)
    {
        //yield return new WaitForSeconds(0.3f);
        //Dices.Clear();

        //for (int i = 0; i < Count; i++)
        //{
        //    Dices.Add(Instantiate(Dice));
        //    Dices[i].transform.position = new Vector3(Random.Range(_generatePos.transform.position.x - 30, _generatePos.transform.position.x + 30), Random.Range(_generatePos.transform.position.y - 30, _generatePos.transform.position.y + 30), _generatePos.transform.position.z);

        //    yield return new WaitForSeconds(0.3f);
        //}

        yield break;
    }

    private void Update()
    {
        if (RestingDices.Count == Dices.Count)
        {
            foreach (BasicDiceScript Dice in RestingDices)
                Dice.StrikeResultNum();

            RestingDices.Clear();
        }
    }

    public void ThrowDices()
    {
        StartCoroutine(ThrowCoroutine());
    }

    IEnumerator ThrowCoroutine()
    {
        float WaitTime = 5f;
        float SpentTime = 0f;

        foreach (BasicDiceScript Dice in Dices)
            Dice.GetComponent<Rigidbody>().useGravity = false;


        while (SpentTime < WaitTime)
        {
            foreach (BasicDiceScript Dice in Dices)
            {
                Dice.GetComponent<Rigidbody>().AddForce((_generatePos.transform.position - Dice.transform.position) * Vector3.Distance(_generatePos.transform.position, Dice.transform.position));
                Dice.GetComponent<Rigidbody>().angularVelocity += new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            }
            SpentTime += Time.deltaTime;
            yield return null;
        }


        foreach (BasicDiceScript Dice in Dices)
        {
            Dice.Throw();
        }
    }

    public void AddRestingDice(BasicDiceScript Dice)
    {
        RestingDices.Add(Dice);
    }
}
