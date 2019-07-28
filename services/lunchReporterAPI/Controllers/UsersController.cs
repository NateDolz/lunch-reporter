using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataModels;
using LunchReporterAPI.Helpers;

namespace LunchReporterAPI.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class UsersController : ControllerBase
  {
    private UsersHelper _UsersHelper { get; }

    public UsersController(UsersHelper helper)
    {
      _UsersHelper = helper;
    }

    [HttpGet]
    public IEnumerable<User> Get()
    {
      return _UsersHelper.GetAllUsers();
    }
  }
}
