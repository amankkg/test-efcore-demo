using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClientApp
{
    class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public decimal Balance { get; set; }

        public List<Post> Posts { get; set; }

        [NotMapped]
        public string FirstName { get => FullName.Split(' ')[0]; }
        [NotMapped]
        public string LastName { get => FullName.Split(' ')[1]; }
    }
}
