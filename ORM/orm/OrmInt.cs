using System;

namespace ORM.orm
{
    public class OrmInt : OrmField
    {
        Func<Orm, int> Getter { get; set; }
        Action<Orm, int> Setter { get; set; }
        public OrmInt(Func<Orm, int> getter, Action<Orm, int> setter)
        {
            Getter = getter;
            Setter = setter;
        }

        public override string GetSQLValue(Orm orm)
        {
            return Getter(orm).ToString();
        }

        public override void SetValue(Orm orm, object value)
        {
            Setter(orm, int.Parse(value.ToString()));
        }
    }
}