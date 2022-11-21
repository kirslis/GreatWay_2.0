using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BasicDiceScript : MonoBehaviour
{
    [SerializeField] List<TextMeshPro> _nums = new List<TextMeshPro>();
    [SerializeField] FlyingNumScript _flyingNum;

    private bool IsThrown;
    private float RestTime = 2f;
    private float TimeInRest = 0f;
    private DicePlace Place;

    private void Awake()
    {
        Place =FindObjectOfType<DicePlace>();
    }

    public void Throw()
    {
        IsThrown = true;
        GetComponent<Rigidbody>().velocity = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), -4) * 10;
        GetComponent<Rigidbody>().useGravity = true;
    }

    private bool IsResting()
    {
        return (GetComponent<Rigidbody>().velocity.x + GetComponent<Rigidbody>().velocity.y + GetComponent<Rigidbody>().velocity.z) < 1;
    }

    private void Update()
    {
        if (IsThrown)
        {
            if (IsResting())
                TimeInRest += Time.deltaTime;

            if (TimeInRest > RestTime)
            {
                Place.AddRestingDice(this);
                RestTime = 1000000000000000000f;
            }
        }
    }

    private TextMeshPro GetHighestNum()
    {
        TextMeshPro highestNum = _nums[0];
        foreach (TextMeshPro num in _nums)
            if (num.transform.position.z < highestNum.transform.position.z)
                highestNum = num;

        return highestNum;
    }

    public void StrikeResultNum()
    {
        StartCoroutine(StrikeCoroutine(GetHighestNum()));
    }

    IEnumerator StrikeCoroutine(TextMeshPro Num)
    {
        Num.color = Color.red;
        yield return new WaitForSeconds(1f);

        FlyingNumScript num = Instantiate(_flyingNum);
        num.transform.position = transform.position;
        num.value = int.Parse(Num.text);
        num.GetComponent<TextMeshPro>().text = Num.text;
        num.GetComponent<TextMeshPro>().color = Color.red;
        num.target = Place.resultViewer.gameObject;
        num.GetComponent<Rigidbody>().velocity = new Vector2(Random.Range(-4,4) * 25, Random.Range(-4,4) * 25);

        Destroy(gameObject);
    }
}
