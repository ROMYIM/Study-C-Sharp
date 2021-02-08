using Domain.Identity.Entities;
using System;

namespace Domain.Identity.Repositories
{
    public class UserRepository
    {
        public User GetUser(string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return new User
            {
                Id = userId,
                UserName = "YIM",
                Password = "123456",
                Role = new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "神",
                    QueryPermissions = new System.Collections.BitArray(new bool[] 
                    {
                        true, false, false, false, false, true, true, true, false, true
                    }),
                    EditPermissions = new System.Collections.BitArray(new bool[] 
                    {
                        true, false, false, false, false, true, true, true, false, true
                    }),
                }
            };
        }
    }
}