using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Race
{
    public int id_bd;
    public string name;
    public float health;
    public float speed;
    public float cadency;
    public float damage;
    public float jumping;

    public enum RaceType { PATO_TORREMOLINOS, PATO_BENALMADENA}
    RaceType raceType;

    public Race()
    {

    }

    public Race(string name, float speed, float cadency, float health, float damage, float jumping, int id)
    {
        this.name = name;
        this.speed = speed;
        this.health = health;
        this.cadency = cadency;
        this.damage = damage;
        this.jumping = jumping;
        this.id_bd = id;

        switch (name)
        {
            case "1":
                raceType = RaceType.PATO_TORREMOLINOS;
                break;
            case "2":
                raceType = RaceType.PATO_BENALMADENA;
                break;
        }
    }
    public string GetClass()
    {
        return name;
    }

    public float GetSpeed()
    {
        return speed;
    }

    public float GetCadency()
    {
        return cadency;
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetDamage()
    {
        return damage;
    }

    public RaceType GetRace()
    {
        return raceType;
    }

    public void SetClass(RaceType raceType)
    {
        this.raceType = raceType;
    }

    public int GetId()
    {
        return id_bd;
    }
}
