using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Models
{
    public class User
    {
        [BindingBehavior(BindingBehavior.Never)]
        public int id { get; set; }
        
        [BindingBehavior(BindingBehavior.Optional)]
        public string firstName { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        public string password { get; set; }


        static public User GetUser(int _id, string _firstName, string _email, string _password) =>
            new User()
            {
                id=_id,
                firstName=_firstName,
                email=_email,
                password=_password
            };
    }
}
