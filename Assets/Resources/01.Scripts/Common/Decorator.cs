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
        dataComponent._defense += this._defense;
        dataComponent._min_magic_attack += this._min_magic_attack;
        dataComponent._max_magic_attack += this._max_magic_attack;
        dataComponent._fire_resist += this._fire_resist;
        dataComponent._cold_resist += this._cold_resist;
        dataComponent._earth_resist += this._earth_resist;
        dataComponent._dark_resist += this._dark_resist;
        dataComponent._poison_resist += this._poison_resist;
        dataComponent._evasion += this._evasion;
    }

    public override void Revert()
    {
        dataComponent._max_hp -= this._max_hp;
        dataComponent._max_mp -= this._max_mp;
        dataComponent._min_attack -= this._min_attack;
        dataComponent._max_attack -= this._max_attack;
        dataComponent._defense -= this._defense;
        dataComponent._min_magic_attack -= this._min_magic_attack;
        dataComponent._max_magic_attack -= this._max_magic_attack;
        dataComponent._fire_resist -= this._fire_resist;
        dataComponent._cold_resist -= this._cold_resist;
        dataComponent._earth_resist -= this._earth_resist;
        dataComponent._dark_resist -= this._dark_resist;
        dataComponent._poison_resist -= this._poison_resist;
        dataComponent._evasion -= this._evasion;
    }
}
