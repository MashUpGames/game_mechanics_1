using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode()]
public class GravityPoint : MonoBehaviour
{
    public float gravityScale=12f, planetRadius=2f, gravityMinRange=1f, gravityMaxRange=2f;
    public GameObject planet, minRS, maxRS;

    // Start is called before the first frame update
    void Update()
    {
        minRS.transform.localScale = new Vector3((planetRadius + gravityMinRange) * 2, (planetRadius + gravityMinRange) * 2, 1);
        maxRS.transform.localScale = new Vector3((planetRadius + gravityMinRange + gravityMaxRange) * 2, (planetRadius + gravityMinRange + gravityMaxRange) * 2, 1);
        GetComponent<CircleCollider2D>().radius = planetRadius + gravityMinRange + gravityMaxRange;
        planet.transform.localScale = new Vector3(1f, 1f, 1f) * planetRadius * 2;
    }

    void OnTriggerStay2D(Collider2D obj)
    {
        float gravitationalPower = gravityScale / planetRadius;
        float dist = Vector2.Distance(obj.transform.position, transform.position);

        if (dist > (planetRadius + gravityMinRange))
        {
            float min = planetRadius + gravityMinRange + 0.5f;
            gravitationalPower = gravitationalPower * (((min + gravityMaxRange) - dist) / gravityMaxRange);
        }

        Vector3 dir = (transform.position - obj.transform.position) * gravitationalPower;
        obj.GetComponent<Rigidbody2D>().AddForce(dir);

        if (obj.CompareTag("Player"))
        {
            obj.transform.up = Vector3.MoveTowards(obj.transform.up, -dir, gravitationalPower * Time.deltaTime * 5f);
        }
    }
}
