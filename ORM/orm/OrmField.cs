namespace ORM.orm
{
    public abstract class OrmField
    {
        public abstract string GetSQLValue(Orm orm);
        public abstract void SetValue(Orm orm, object value);
    }
}