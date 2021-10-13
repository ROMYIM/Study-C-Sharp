using Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MiddleWareSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public IEnumerable<Menu> Menus()
        {
            return new List<Menu>
            {
                new Menu
                {
                    Name = "订单管理",
                    Key = "OrderManager",
                    Uri = string.Empty,
                    Children = new List<Menu> 
                    {
                        new Menu
                        {
                            Name = "订单信息",
                            Uri = "/order",
                            Key = "Orders"
                        },

                        new Menu
                        {
                            Name = "收货信息",
                            Uri = "/receive",
                            Key = "Receive"
                        }
                    }
                },

                new Menu
                {
                    Name = "客户管理",
                    Key = "ClientManager",
                    Uri = string.Empty,
                    Children = new List<Menu> 
                    {
                        new Menu
                        {
                            Name = "客户信息",
                            Uri = "/client",
                            Key = "Clients"
                        },

                        new Menu
                        {
                            Name = "账单信息",
                            Uri = "/bill",
                            Key = "Bills"
                        }
                    }
                }
            };
        }
    }
}