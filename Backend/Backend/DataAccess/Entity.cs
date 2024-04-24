namespace Backend.DataAccess
{
    public abstract class Entity
    {
        #region Data
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsActive { get; set; }
        #endregion
    }
}
