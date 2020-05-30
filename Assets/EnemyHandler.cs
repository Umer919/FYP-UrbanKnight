using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandler : MonoBehaviour
{
    Animator _anim;
    public int MoveSpeed,EnemyHealth;
    public float AttackDistance;
    bool _isAttack, _IsDeath,Iskill;
    playerController _controller;
    void Start()
    {
        _anim = GetComponent<Animator>();
        Iskill = false;
        _controller = GameHandler._Instance.clsDataTypes._Player.GetComponent<playerController>();
    }

    void Update()
    {
        FncMoveEnemy();
        if(Input.GetKeyDown(KeyCode.A))
        {
              Iskill = true;
                    _IsDeath = true;
                    _isAttack = false;
                    FncAnimationHandler();
                    FncVanished();
                    GameHandler._Instance.clsDataTypes.killZombie++;
                    GameHandler._Instance.clsDataTypes.RenamingEnemy--;
                    GameHandler._Instance.FncRandom();
        }
    }

    void FncMoveEnemy()
    {
        if (!_IsDeath && _controller.grounded == true)
        {
            float dis = Vector3.Distance(transform.position, GameHandler._Instance.clsDataTypes._Player.position);
            if (dis > AttackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, GameHandler._Instance.clsDataTypes._Player.position, Time.deltaTime * MoveSpeed);
                transform.LookAt(GameHandler._Instance.clsDataTypes._Player);
                _isAttack = false;
            }
            else
            {
                _isAttack = true;
            }
            FncAnimationHandler();
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Bullet")
        {
            
            if (EnemyHealth >0)
            {
                EnemyHealth -= 10;
            }

            if(EnemyHealth<=0)
            {
                if (Iskill == false)
                {
                    Iskill = true;
                    _IsDeath = true;
                    _isAttack = false;
                    FncAnimationHandler();
                    FncVanished();
                    GameHandler._Instance.clsDataTypes.killZombie++;
                    GameHandler._Instance.clsDataTypes.RenamingEnemy--;
                    GameHandler._Instance.FncRandom();
                }
            }
     
           
        }
    }


    void FncAnimationHandler()
    {
        if (_isAttack)
        {
            if (_anim.GetBool("IsAttack") == false)
                _anim.SetBool("IsAttack", true);
        }
        if (!_isAttack)
        {
            if (_anim.GetBool("IsAttack") == true)
                _anim.SetBool("IsAttack", false);
        }

        if (_IsDeath)
        {
            {
                if (_anim.GetBool("IsDeath") == false)
                {
                    _anim.SetBool("IsAttack", false);
                    _anim.SetBool("IsDeath", true);
                }
            }
           
        }
    }

    void FncVanished()
    {
        Destroy(this.gameObject, 0.0f);
    }
}
