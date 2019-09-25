using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Service.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]//获取用户数量
        public ActionResult Get() {
            return Json(DAL.UserInfo.Instance.GetCount());
        }
        [HttpPut("{username}")]//获取单用户数据
        public ActionResult getUser(string username) {
            var result = DAL.UserInfo.Instance.GetModel(username);
            if (result != null)
                return Json(Result.Ok(result));
            else
                return Json(Result.Err("用户名不存在"));
        }


        // GET: api/<controller>
   

        // GET api/<controller>/5
       

        // POST api/<controller>
      [HttpPost]
        public ActionResult Post([FromBody]Model.UserInfo users)
        {
            try
            {
                int n = DAL.UserInfo.Instance.Add(users);
                return Json(Result.Ok("添加成功"));
            }
            catch (Exception ex) {
                if (ex.Message.ToLower().Contains("primary"))
                    return Json(Result.Err("用户已存在"));
                else if (ex.Message.ToLower().Contains("null"))
                {
                    return Json(Result.Err("用户名，密码，身份证不能为空"));
                    
                }
                else
                    return Json(Result.Err(ex.Message));
            }
        }

        // PUT api/<controller>/5
   

        // DELETE api/<controller>/5
        [HttpDelete("{username}")]
        public ActionResult Delete(string username)
        {
            try {
                var n = DAL.UserInfo.Instance.Delete(username);
                if (n != 0) {
                    return Json(Result.Ok("删除成功"));
                }
                else
                    return Json(Result.Err("用户名不存在"));
            }
            catch (Exception ex) {
                if (ex.Message.ToLower().Contains("foreign"))
                {
                    return Json(Result.Err("发布了作品或活动的用户不能删除"));
                }
                else
                    return Json(Result.Err(ex.Message));
            }
        }
    }
}
