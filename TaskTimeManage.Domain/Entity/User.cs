using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskTimeManage.Domain.Entity
{
    public class User
    {
        public User(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }

        public User(int id, string userName, string password, DateTime creationTime)
        {
            Id = id;
            UserName = userName;
            Password = password;
            CreationTime = creationTime;
        }

        public int Id
        {
            get; set;
        }
        public string UserName
        {
            get; set;
        }
        public string Password
        {
            get; set;
        }
        public DateTime CreationTime
        {
            get; set;
        }
    }
}
