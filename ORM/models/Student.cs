using ORM.orm;

namespace ORM.models
{
    public class Student : Orm
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public int ClassId { get; set; }

        static Student()
        {
            Int("Students", "Id",
                (student) => (student as Student).Id,
                (student, value) => (student as Student).Id = value);
            
            String("Students", "FirstName", (student) => (student as Student).FirstName,
                (student, value) => (student as Student).FirstName = value);
            
            String("Students", "LastName", (student) => (student as Student).LastName,
                (student, value) => (student as Student).LastName = value);
            
            Int("Students", "Age", (student) => (student as Student).Age,
                (student, value) => (student as Student).Age = value);
            
            Int("Students", "ClassId", (student) => (student as Student).ClassId,
                (student, value) => (student as Student).ClassId = value);

            PrimaryKey("Students", "Id");
        }

        protected override string TableName()
        {
            return "Students";
        }
    }
}