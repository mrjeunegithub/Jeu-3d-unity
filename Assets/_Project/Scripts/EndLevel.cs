using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    public Transform flag;
    public Transform endFlag;
    //Vector3 flagPosition;
    Vector3 endPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        endPosition = endFlag.position;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("contact Player");
            //StartCoroutine("RaisedFlag");
        }
    }


    IEnumerator RaisedFlag()
    {
        yield return new WaitForSeconds(.1f);
        while(Vector3.Distance(endPosition, flag.position) <= 0.2f)
        {
            flag.position += new Vector3(0, 10 * Time.deltaTime, 0);
            yield return new WaitForSeconds(.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
