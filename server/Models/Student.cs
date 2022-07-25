using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Student
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [RegularExpression(@"^[\- א-ת]{3,15}$")]
        public string School { get; set; }
        [Required]
        [RegularExpression(@"^[\- א-ת]{3,15}$")]
        public string LastName { get; set; }
        [Required]
        [RegularExpression(@"^[\- א-ת]{3,15}$")]
        public string FirstName { get; set; }
        [Required]
        public int MaleOrFemale { get; set; }
        [Required]
        [RegularExpression(@"^0[0-8]{1}-[0-9]{7}$")]
        public string HomePhone { get; set; }
        [Required]
        [RegularExpression(@"^05[0-8]{1}-[0-9]{7}$")]
        public string MobilePhone { get; set; }
        [Required]
        [RegularExpression(@"^([\w-]+(?:\.[\w-]+)*)@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$")]
        public string Email { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        [RegularExpression(@"^[\- א-ת]{3,15}$")]
        public string CountryOfBirth { get; set; }
        public DateTime? ImmigrationDate { get; set; }
        [Required]
        [RegularExpression(@"^[\- א-ת]{3,15}$")]
        public string Nation { get; set; }

    }
}
