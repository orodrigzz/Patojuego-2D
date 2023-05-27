public class Race
{
    public int id_race;
    public float health;
    public float damage;
    public float speed;
    public float jumping;
    public float cadency;
    public string name;

    public Race(int _id_race, float _health, float _damage, float _speed, float _jumping, float _cadency, string _name)
    {
        id_race = _id_race;
        name = _name;
        health = _health;
        damage = _damage;
        cadency = _cadency;
        speed = _speed;
        jumping = _jumping;
    }
}
