using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Decorator : DecoratorDataComponent
{
    protected IDecorator decorator;
    protected DecoratorDataComponent dataComponent;

    public Decorator Set(IDecorator decorator, DecoratorDataComponent dataComponent)
    {
        this.decorator = decorator;
        this.dataComponent = dataComponent;
        return this;
    }

    public override void Operation()
    {
        decorator.Operation();
        dataComponent._max_hp += this._max_hp;
        dataComponent._max_mp += this._max_mp;
        dataComponent._min_attack += this._min_attack;
        dataComponent._max_attack += this._max_attack;
        dataComponent._defence += this._defence;
        dataComponent._min_magic_attack += this._min_magic_attack;
        dataComponent._max_magic_attack += this._max_magic_attack;
        dataComponent._fire_res += this._fire_res;
        dataComponent._cold_res += this._cold_res;
        dataComponent._earth_res += this._earth_res;
        dataComponent._dark_res += this._dark_res;
        dataComponent._poison_res += this._poison_res;
        dataComponent._avoid += this._avoid;
    }

    public override void Revert()
    {
        dataComponent._max_hp -= this._max_hp;
        dataComponent._max_mp -= this._max_mp;
        dataComponent._min_attack -= this._min_attack;
        dataComponent._max_attack -= this._max_attack;
        dataComponent._defence -= this._defence;
        dataComponent._min_magic_attack -= this._min_magic_attack;
        dataComponent._max_magic_attack -= this._max_magic_attack;
        dataComponent._fire_res -= this._fire_res;
        dataComponent._cold_res -= this._cold_res;
        dataComponent._earth_res -= this._earth_res;
        dataComponent._dark_res -= this._dark_res;
        dataComponent._poison_res -= this._poison_res;
        dataComponent._avoid -= this._avoid;
    }
}
