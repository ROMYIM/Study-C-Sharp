using Domain.Identity.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Identity.Repositories
{
    public class UserRepository
    {
        private readonly Dictionary<string, User> _users = new Dictionary<string, User>
        {
            { "yim", new User
                {
                    Id = "yim",
                    UserName = "YIM",
                    Password = "123456",
                    Role = new Role
                    {
                        Id = Guid.NewGuid().ToString(),
                        Name = "荣儿",
                        QueryPermissions = new System.Collections.BitArray(new bool[] 
                        {
                            true, false, false, false, false, true, true, true, false, true
                        }),
                        EditPermissions = new System.Collections.BitArray(new bool[] 
                        {
                            true, false, false, false, false, true, true, true, false, true
                        }),
                    }
                }
            }
        };

        public User GetUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return _users[userId];
        }
    }
}