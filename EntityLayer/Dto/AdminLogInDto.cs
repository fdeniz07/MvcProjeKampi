namespace EntityLayer.Dto
{
    public class AdminLogInDto
    {
        public string AdminUserName { get; set; }
        public string AdminMail { get; set; }
        public string AdminPassword { get; set; }
        public int AdminRoleId { get; set; }
        public int AdminStatusId { get; set; }
    }
}
