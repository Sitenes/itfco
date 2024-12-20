using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IdentityModel.Protocols.WSTrust;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toplearn.DataLayer.Entities.Courses
{
    public class Cart
    {
        [Key]
        public int Id { get; set; }

        //  Details
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string AddressAdditional { get; set; }  // Optional field for apartment, suite, etc.
        public string City { get; set; }
        public string State { get; set; }
        public string Postcode { get; set; }
        public string Notes { get; set; }  // Special notes for shipping, if any
        public bool IsPaid { get; set; }
        public Guid? UserCreatorId { get; set; }

        #region Relations

        [ForeignKey("UserCreatorId")]
        public User.User UserCreator { get; set; }

        public List<Course> Courses { get; set; }
        #endregion
    }
}
