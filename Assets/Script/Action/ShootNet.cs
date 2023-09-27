using Fusion;
using System.Collections;
using UnityEngine;
using Zenject.SpaceFighter;

public class ShootNet : NetworkBehaviour
{
    public static ShootNet Instance;
    [Networked(OnChanged = nameof(OnShootChanged))] public bool IsShoot { get; set; }


    private GameObject _bullet;
    private Transform _outPoint;

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
            var gg=Instantiate(_bullet, _outPoint.position, _outPoint.rotation);
            Debug.Log($"Выстрел... {changed.Behaviour.IsShoot} {gg.gameObject.name}");
        }
    }

    public void Shoot(GameObject bullet, Transform outPoint)
    {
        _bullet = bullet;
        _outPoint = outPoint;
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