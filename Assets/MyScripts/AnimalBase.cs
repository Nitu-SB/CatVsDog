using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Unity.VisualScripting;

public class AnimalBase : MonoBehaviour
{
    public BelongCamp belongCamp;
    public float health;//生命值
    public float maxHealth;//最大生命值
    public float rotateSpeed;//旋转速度
    public float moveForce;//移动力度

    public bool isDie;
    public bool isFlash;
    public bool isReady;
    public GameObject getHurtEffect,dieEffct;

    private SpriteRenderer mySprite;
    private Rigidbody2D rb;

    public float trackEnemyTime = 3f;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        mySprite.color = Global.Instance.colorDict[belongCamp];

        rb = GetComponent<Rigidbody2D>();

        rb.drag = 3f;
        rb.angularDrag = 3f;
        rb.AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)).normalized * 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDie)
        {
            return;
        }
        if (!isReady)
        {
            return;
        }
        if (Mathf.Abs(rb.angularVelocity) < rotateSpeed)
        {
            StartCoroutine(AddRotateSpeed());
        }
        if (rb.velocity.magnitude < moveForce * 0.8f)
        {
            StartCoroutine(AddSpeed());
        }
        if (rb.velocity.magnitude > moveForce)
        {
            rb.velocity = rb.velocity.normalized * moveForce;
        }

        if (GameManager.instance.trackMode)
        {
            timer += Time.deltaTime;
            if (timer > trackEnemyTime)
            {
                timer = 0;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 99); // 获取半径内的所有碰撞体

                foreach (Collider2D col in colliders)
                {
                    Debug.Log(">>>>>>>>");
                    if (col.gameObject.tag == "Body" && col.gameObject.GetComponent<AnimalBase>().belongCamp != belongCamp) // 如果tag是"Body"且不是同阵营
                    {
                        Debug.Log("找到不同阵营的Body");
                        GameObject enemy = col.gameObject;
                        StartCoroutine(TrackEnemy(enemy));
                        break;
                    }
                }

            }
        }
    }
    public void StartGame()
    {
        isReady = true;
        rb.drag = 0;
        rb.angularDrag = 0.05f;
        rb.AddTorque(rotateSpeed);
        rb.AddForce(new Vector2(UnityEngine.Random.Range(-10f, 10f), UnityEngine.Random.Range(-10f, 10f)).normalized * 200f);
    }
    /// <summary>
    /// 受到伤害
    /// </summary>
    /// <param name="damage">伤害值</param>
    public virtual void GetHurt(float damage)
    {
        if (isFlash)
        {
            return;
        }

        GameObject effect= Instantiate(getHurtEffect);
        effect.transform.localScale = Vector3.one * 2f;
        effect.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
        Destroy(effect, 3f);
        GetComponent<SpriteRenderer>().color = Color.white;
        DOVirtual.DelayedCall(0.3f, () => {

            isFlash = false;
            if (!isDie)
            {
                mySprite.color = Global.Instance.colorDict[belongCamp];
            }
            
        });
        health -= damage;

        isFlash = true;
        if (health <= 0)
        {
            GameManager.instance.AnimalDie(belongCamp);
            DieAnim(this.gameObject, true);
            DieAnim(transform.GetChild(0).gameObject);
            isDie = true;
            GameObject dieEffect = Instantiate(dieEffct);
            dieEffect.transform.localScale = Vector3.one * 2f;
            dieEffect.transform.position = new Vector3(transform.position.x, transform.position.y, -2);
            Destroy(dieEffect, 3f);
            return;
        }
        
    }
    bool isAddRotate = false;
    IEnumerator AddRotateSpeed()
    {
        if (isAddRotate)
        {

        }
        else
        {

            if (Mathf.Abs(rb.angularVelocity) < rotateSpeed)
            {
                isAddRotate = true;
                for (int i = 0; i < 5; i++)
                {
                    if (Mathf.Abs(rb.angularVelocity) < rotateSpeed)
                    {
                        rb.AddTorque(rotateSpeed);
                    }
                    yield return new WaitForSeconds(0.2f);
                }

            }

            isAddRotate = false;
        }

    }
    IEnumerator AddSpeed()
    {
        for (int i = 0; i < 5; i++)
        {
            if (rb.velocity.magnitude < moveForce)
            {
                rb.AddForce(rb.velocity.normalized);
            }
            yield return new WaitForSeconds(0.2f);

        }
    }
    IEnumerator TrackEnemy(GameObject Obj)
    {
        
        for (int i = 0; i < 30; i++)
        {
            if (!isDie)
            {
                rb.AddForce((Obj.transform.position - transform.position).normalized * 50f);
                //Debug.Log("歼敌加速！");
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //对敌人造成随机伤害
        if (collision.gameObject.GetComponent<AnimalBase>() != null)
        {
            AnimalBase enemy = collision.gameObject.GetComponent<AnimalBase>();
            if(enemy.belongCamp != this.belongCamp)
            {
                enemy.GetHurt(Random.Range(Global.Instance.minDamage, Global.Instance.maxDamage));
            }
        }
    }

    public void DieAnim(GameObject Obj, bool IsOriginColor = false)
    {
        Color campColor = Global.Instance.colorDict[belongCamp];
        if (Obj.GetComponent<Rigidbody2D>() != null)
        {
            Obj.GetComponent<Rigidbody2D>().drag = 3;
            Obj.GetComponent<Rigidbody2D>().angularDrag = 3;
            Obj.GetComponent<Collider2D>().enabled = false;
        }
        

        if (Obj.GetComponent<BoxCollider2D>() != null)
        {
            Obj.GetComponent<BoxCollider2D>().enabled = false;
        }

        if (Obj.GetComponent<Joint2D>() != null)
        {
            Obj.GetComponent<Joint2D>().enabled = false;
        }
        Obj.GetComponent<SpriteRenderer>().sortingOrder = Obj.GetComponent<SpriteRenderer>().sortingOrder - 2;
        Obj.transform.DOScale(new Vector3(Obj.transform.localScale.x * 0.7f, Obj.transform.localScale.y * 0.7f, Obj.transform.localScale.z * 0.7f), 1f);
        if (IsOriginColor)
        {
            //color32 （0,255）从全局设置获取颜色；
            Obj.GetComponent<SpriteRenderer>().color = new Color(campColor.r * 0.25f, campColor.g * 0.25f, campColor.b * 0.25f, 1);
        }
        else
        {
            //color （0,1）一般为白色；
            Obj.GetComponent<SpriteRenderer>().color = new Color(Obj.GetComponent<SpriteRenderer>().color.r * 0.3f, Obj.GetComponent<SpriteRenderer>().color.g * 0.3f, Obj.GetComponent<SpriteRenderer>().color.b * 0.3f, 1);
        }


        if (Obj.transform.Find("Shadow") != null)
        {
            Obj.transform.Find("Shadow").gameObject.SetActive(false);
        }

    }
}
