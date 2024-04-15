namespace Backend.Models
{
    public class User
    {
        #region Properties
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        #endregion
    }
}
