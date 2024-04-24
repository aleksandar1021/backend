namespace Backend.DataAccess
{
    public class User : Entity
    {
        #region Properties

        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }
        public virtual ICollection<Role> Role { get; set; } = new HashSet<Role>();
        #endregion
    }
}
