using Fusion;
using System.Collections;
using UnityEngine;
using Zenject.SpaceFighter;
using static UnityEditor.Experimental.GraphView.GraphView;

public class ShootNet : NetworkBehaviour
{
    public static ShootNet Instance;
    [Networked(OnChanged = nameof(OnShootChanged))] public bool IsShoot { get; set; }

    private GameObject _bullet;
    private Transform _outPoint;
    private ParticleSystem _gunExitParticle;
    private ShootPlayer _player;
    private void Start()
    {
        _player = GetComponent<ShootPlayer>();
        _bullet = _player.bullet;
        _outPoint = _player.outBullet;
        _gunExitParticle = _player.gunExitParticle;
    }
    private static void OnShootChanged(Changed<ShootNet> changed)
    {
        //Debug.Log($"Выстрел... {changed.Behaviour.IsShoot}");
        //
        bool isChanged = changed.Behaviour.IsShoot;
        changed.LoadOld();
        bool isChangedOld = changed.Behaviour.IsShoot;

        if (isChanged && !isChangedOld)//методика запуска из под статики
        {
            changed.Behaviour.OnShoot(changed);
        }

    }

    private void OnShoot(Changed<ShootNet> changed)//дубль в сети
    {
        if (!Object.HasInputAuthority)
        {
            _gunExitParticle.Play();
            Instantiate(_bullet, _outPoint.position, _outPoint.rotation);
            
            //Debug.Log($"Выстрел... {changed.Behaviour.IsShoot} {gg.gameObject.name}");
            Debug.Log($"Выстрел... {changed.Behaviour.IsShoot}");
        }
    }

    public void Shoot()
    {
        
        //Instantiate(_bullet, _outPoint.position, _outPoint.rotation);
        StartCoroutine(ShootUpDate());
    }

    private IEnumerator ShootUpDate()
    {
        IsShoot = true;
        yield return new WaitForSeconds(0.1f);
        IsShoot = false;
    }

    //public override void FixedUpdateNetwork()
    //{
    //    base.FixedUpdateNetwork();
    //}


}