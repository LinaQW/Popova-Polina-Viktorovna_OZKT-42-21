//using PopovaPolinaOZKT_42_21.DataBase.Models;

using PopovaPolinaOZKT_42_21.DataBase.Models;

namespace PopovaPolinaOZKT_42_21.Tests
{
    public class GroupTests
    {
        [Fact]
        public void IsValidGroupName_KT4221_True()
        {
            //arrange
            var testGroup = new Group
            {
                GroupName = "KT-42-21"
            };
            //act
            var result = testGroup.IsValidGroupName();
            //assert
            Assert.True(result);
        }
    }
}