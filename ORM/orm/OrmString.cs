using System;

namespace ORM.orm
{
    public class OrmString : OrmField
    {
        Func<Orm, string> Getter { get; set; }
        Action<Orm, string> Setter { get; set; }
        public OrmString(Func<Orm, string> getter, Action<Orm, string> setter)
        {
            Getter = getter;
            Setter = setter;
        }

        public override string GetSQLValue(Orm orm)
        {
            return "'"+Getter(orm).ToString()+"'";
        }
        public override void SetValue(Orm orm, object value)
        {
            Setter(orm, value as string);
        }
    }
}