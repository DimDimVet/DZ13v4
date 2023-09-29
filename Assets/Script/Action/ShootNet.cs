using Fusion;
using System.Collections;
using UnityEngine;

public class ShootNet : NetworkBehaviour
{
    public static ShootNet Instance;
    //создадим переменую на сервере, в случае ее изменения в сети OnChanged,
    //она будет запускать метод OnShootChanged (у всех клиентов) с аргуметом Changed<ShootNet>
    //НО условие, запускаемый метод статик
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
        //проверка реального состояния обработки на сервере
        bool isChanged = changed.Behaviour.IsShoot;//получим текущее значение переменой
        changed.LoadOld();//попросим сервер дать нам старое(прошлое)значение переменой
        bool isChangedOld = changed.Behaviour.IsShoot;//запишем старое значение
        //в итоге имеем текущее значение и старое, дабы синхронизировать события в работе клиента и сервера
        //сравним этизначения переменных - если они инверсны значит событие изменения имеется, если они одинаковы значит события нет
        if (isChanged && !isChangedOld)
        {
            changed.Behaviour.OnShoot(changed);//и в случае true, запустим через конструкцию changed.Behaviour не статичный метод
            //через аргумент можно если нужно передаем саму переменную
        }
    }

    //а в методе можно реализовать логику работы у все участников (и сервер и клиента)
    private void OnShoot(Changed<ShootNet> changed)//дубль в сети
    {
        NetworkObject gg = Object;
        //используем сетевой компонент Fusion - Object, и определим данного клиента
        //если мы не источник события, тогда мы клиент, далее какая то логика
        if (!Object.HasInputAuthority)
        {
            _gunExitParticle.Play();
            Instantiate(_bullet, _outPoint.position, _outPoint.rotation);
            
            Debug.Log($"Выстрел... {changed.Behaviour.IsShoot}");
        }
    }

    private IEnumerator ShootUpDate()
    {
        IsShoot = true;//отправим на сервер новое значение
        yield return new WaitForSeconds(0.1f);//ждем принятие сервером изменений
        IsShoot = false;//отправим на сервер новое(измененое) значение

        //данные изменения переменой IsShoot будут запускать OnShootChanged
    }
    public void Shoot()
    {
        
        StartCoroutine(ShootUpDate());
    }



}