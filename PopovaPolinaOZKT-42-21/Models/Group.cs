using System.Text.RegularExpressions;

namespace PopovaPolinaOZKT_42_21.DataBase.Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public bool IsValidGroupName()
        {
            return Regex.Match(GroupName, @"\D*-\d*-\d\d").Success;
        }
    }
}
