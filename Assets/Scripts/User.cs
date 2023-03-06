using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User 
{
    public string nickname;
    public int db_id;
    public int id_race;
    public Race races;

    public User()
    {

    }

    public User(string username, int idDB, int idClass)
    {
        this.nickname = username;
        this.db_id = idDB;
        this.id_race = idClass;
    }

    public string GetnicknameName()
    {
        return nickname;
    }

    public int GetId()
    {
        return db_id;
    }

    public void SetRace(Race _race)
    {
        this.races = _race;
    }

    public Race GetRace()
    {
        return races;
    }

    public int Getid_race()
    {
        return id_race;
    }
}
